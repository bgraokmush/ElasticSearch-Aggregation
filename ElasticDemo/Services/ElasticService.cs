using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Transport;
using ElasticDemo.Models;

namespace ElasticDemo.Services
{
    public class ElasticService
    {

        private readonly ElasticsearchClient _client;
        private const int MAX_GROUP_COUNT = 10000;

        public ElasticService()
        {
            var hostName = "https://localhost:9200";
            var _elasticUserName = "elastic";
            var _elasticPassword = "zwiYwsIPxV=k_RQbvU7U";
            var _certificateFingerprint = "bc9ccfd1120a26907f2497a557cc9164c5f63db67a7712d934a2ef323523d25e";
            var _elasticBaseUri = new Uri(hostName);

            var settings = new ElasticsearchClientSettings(_elasticBaseUri)
                                    .Authentication(new BasicAuthentication(_elasticUserName, _elasticPassword))
                                    .ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true)
                                    .MaximumRetries(3)
                                    .DefaultFieldNameInferrer(p => p);

            _client = new ElasticsearchClient(settings);
        }


        public List<ErrorLogViewModel> GetErrorLogGrouped(SearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
                throw new ArgumentNullException(nameof(searchCriteria));

            List<ErrorLogViewModel> resultSearchQuery = new List<ErrorLogViewModel>();

            var searchDescriptor = CreateSearchDescriptor(searchCriteria);


            searchDescriptor.Aggregations(a => a
               .Terms("group_InsuranceCompany", ic => ic
                    .Size(MAX_GROUP_COUNT)
                    .Field(f => f.InsuranceCompany.Suffix("keyword"))
                        .Aggregations(aa => aa
                            .Terms("group_Exception", ex => ex
                                .Field(f => f.Exception.Suffix("keyword"))
                                    .Aggregations(aaa => aaa
                                        .Terms("group_Messages", msg => msg
                                            .Field(f => f.Message.Suffix("keyword"))
                                                .Aggregations(aaaa => aaaa
                                                    .Max("max_log_date", m => m
                                                        .Field(f => f.DateTime)
                                                    )
                                                )
                                        )
                                    )
                            )
                        )
               )
            );

            var response = _client.Search<ErrorLogModel>(searchDescriptor);

            if (!response.IsValidResponse)
                throw new InvalidOperationException("Elasticsearch response is not valid");

            var insuranceCompany = response.Aggregations["group_InsuranceCompany"] as StringTermsAggregate;

            foreach (var icItem in insuranceCompany.Buckets)
            {
                var icValue = icItem.Key.ToString();
               
                var exception = icItem.Values.First() as StringTermsAggregate;

                foreach (var exItem in exception.Buckets)
                {
                    var exValue = exItem.Key.ToString();

                    var message = exItem.Values.First() as StringTermsAggregate;

                    foreach (var msgItem in message.Buckets)
                    {
                        var msgValue = msgItem.Key.ToString();
                        var count = msgItem.DocCount;

                        var lastErrorDateAsTimeSpan = (msgItem.First(x => x.Key == "max_log_date").Value as MaxAggregate).Value;

                        var maxLogDate = DateTimeOffset.FromUnixTimeMilliseconds((long)lastErrorDateAsTimeSpan).LocalDateTime;

                        resultSearchQuery.Add(new()
                        {
                            InsuranceCompany = icValue,
                            Exception = exValue,
                            Message = msgValue,
                            LastErrorDate = maxLogDate,
                            TotalCount = count
                        });
                    }
                }

            }

            resultSearchQuery = resultSearchQuery.OrderByDescending(x => x.TotalCount).ToList();

            return resultSearchQuery.ToList();

        }
        private static SearchRequestDescriptor<ErrorLogModel> CreateSearchDescriptor(SearchCriteria criteria)
        {

            #region Criteria Validation
            var queryDescriptorsMust = new List<Action<QueryDescriptor<ErrorLogModel>>> { }; //skorlamaya dahil olacak filtreler
            var queryDescriptorsFilter = new List<Action<QueryDescriptor<ErrorLogModel>>> { }; //skorlamaya dahil olmayacak filtrelemeler icin 
            var queryDescriptorsMustNot = new List<Action<QueryDescriptor<ErrorLogModel>>> { }; //skorlamaya dahil olmayacak filtrelemeler icin

            if (!string.IsNullOrEmpty(criteria.InsuranceCompany))
                queryDescriptorsMust.Add(q => q.Match(m => m.Field(f => f.InsuranceCompany.Suffix("keyword")).Query(criteria.InsuranceCompany)));

            if (!string.IsNullOrEmpty(criteria.Exception))
                queryDescriptorsMust.Add(q => q.Match(m => m.Field(f => f.Exception.Suffix("keyword")).Query(criteria.Exception)));

            queryDescriptorsFilter.Add(m => m.Range(r => r.DateRange(f => f.Field(fi => fi.DateTime).Gte(criteria.BeginDate).TimeZone("+03:00"))));

            queryDescriptorsFilter.Add(m => m.Range(r => r.DateRange(f => f.Field(fi => fi.DateTime).Lte(criteria.EndDate).TimeZone("+03:00"))));

            queryDescriptorsFilter.Add(m => m.Exists(e => e.Field(f => f.Message.Suffix("keyword"))));

            queryDescriptorsMustNot.Add(m => m.Term(t => t.Field(f => f.Message.Suffix("keyword")).Value("")));

            #endregion



            var indexName = "error_logs*";

            var searchDescriptor = new SearchRequestDescriptor<ErrorLogModel>(indexName);

            searchDescriptor
                .Size(0)
                .Query(q => q
                .Bool(b => b
                 .Filter(queryDescriptorsFilter.Any() ? queryDescriptorsFilter.ToArray() : null)
                 .Must(queryDescriptorsMust.Any() ? queryDescriptorsMust.ToArray() : null)
                 .MustNot(queryDescriptorsMustNot.Any() ? queryDescriptorsMustNot.ToArray() : null)
                 ));



            return searchDescriptor;
        }
    }

}
