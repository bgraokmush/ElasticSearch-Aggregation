

using ElasticDemo.Models;
using ElasticDemo.Services;

var searchCriteria = new SearchCriteria
{
    BeginDate = new DateTime(2024, 9, 24),
    EndDate = new DateTime(2024, 9, 26)
};

var elasticService = new ElasticService();

var result = elasticService.GetErrorLogGrouped(searchCriteria);

foreach (var item in result)
{
    Console.WriteLine($"InsuranceCompany: {item.InsuranceCompany}, Exception: {item.Exception}, Message: {item.Message}, TotalCount: {item.TotalCount}, LastErrorDate: {item.LastErrorDate}");
}
