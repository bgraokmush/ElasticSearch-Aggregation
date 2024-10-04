<div id="top"></div>

<br />
<div align="center">
  <h3 align="center">ElasticSearch Aggregation Project</h3>

  <p align="center">
    🚀 An application built using .NET Core 8 for performing data aggregation with ElasticSearch.
    <br />
    <a href="https://github.com/bgraokmush/ElasticSearch-Aggregation"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/bgraokmush/ElasticSearch-Aggregation">View Demo</a>
    ·
    <a href="https://github.com/bgraokmush/ElasticSearch-Aggregation/issues">Report Bug</a>
    ·
    <a href="https://github.com/bgraokmush/ElasticSearch-Aggregation/issues">Request Feature</a>
  </p>
</div>

## 📌 About The Project

This project demonstrates how to use ElasticSearch for data aggregation using a sample `error_log` structure. It is built with .NET Core 8 and utilizes ElasticSearch's aggregation features to group and retrieve data. 

Mock data is generated and fed into the system using ElasticSearch DevTools.

<p align="right">(<a href="#top">back to top</a>)</p>

### 🛠 Built With

* [.NET Core 8](https://learn.microsoft.com/en-us/dotnet/core/)
* [ElasticSearch](https://www.elastic.co/elasticsearch/)
* [Kibana](https://www.elastic.co/kibana/)

<p align="right">(<a href="#top">back to top</a>)</p>

## 🚀 Getting Started

To get a local copy up and running, follow these steps.

### Prerequisites

* Ensure you have ElasticSearch and Kibana running. You can either use a locally installed instance or one running in Docker.
* Install the necessary packages using the following commands.

### Installation

1. Clone the repo:
   ```sh
   git clone https://github.com/bgraokmush/ElasticSearch-Aggregation.git
   ```
2. Install the required .NET dependencies:
   ```sh
   dotnet add package Elastic.Clients.Elasticsearch --version 8.11.0
   dotnet add package Elastic.Transport --version 8.11.0
   ```
3. Run the application:
   ```sh
   dotnet run
   ```

You can now interact with the system and send aggregation queries via the integrated APIs.

<p align="right">(<a href="#top">back to top</a>)</p>

## 📊 Usage

This project groups error logs and retrieves aggregated data using ElasticSearch's Aggregation API. Mock data for testing can be inserted using the DevTools in Kibana. 

_For detailed examples, please refer to the [Documentation](https://github.com/bgraokmush/ElasticSearch-Aggregation)_.

<p align="right">(<a href="#top">back to top</a>)</p>

## 🛣 Roadmap

- [x] Initial setup with ElasticSearch and Kibana
- [x] Aggregation queries for grouping data
- [ ] Improve error logging mechanism
- [ ] Add more detailed query examples for complex aggregations

See the [open issues](https://github.com/bgraokmush/ElasticSearch-Aggregation/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#top">back to top</a>)</p>

## 💡 Contributing

Contributions are what make the open-source community amazing! Feel free to make this project better by following these steps:

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

Any enhancements, bug fixes, or feature additions are greatly appreciated!

<p align="right">(<a href="#top">back to top</a>)</p>

## 📄 License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#top">back to top</a>)</p>

## ✉️ Contact

Eda - [@your_twitter](https://twitter.com/your_username) - email@example.com

Project Link: [https://github.com/bgraokmush/ElasticSearch-Aggregation](https://github.com/bgraokmush/ElasticSearch-Aggregation)

<p align="right">(<a href="#top">back to top</a>)</p>

## 🙏 Acknowledgments

* [ElasticSearch Documentation](https://www.elastic.co/guide/en/elasticsearch/reference/current/index.html)
* [Kibana Documentation](https://www.elastic.co/guide/en/kibana/current/index.html)

<p align="right">(<a href="#top">back to top</a>)</p>
