# TracePlot [![.NET](https://github.com/simidt/TracePlot/actions/workflows/dotnet.yml/badge.svg)](https://github.com/simidt/TracePlot/actions/workflows/dotnet.yml)[![Node.js CI](https://github.com/simidt/TracePlot/actions/workflows/node.js.yml/badge.svg)](https://github.com/simidt/TracePlot/actions/workflows/node.js.yml)
TracePlot is an ASP.NET core application for performing and plotting traceroutes to a specified host over long periods of time in order to identify network
instabilities like latency spikes. The collected data can be viewed through a React-based frontend, which also allows for starting new traceroutes. Traceplot is still in development.

***Due to ![bugs](https://github.com/dotnet/runtime/issues/927) in dotnet's Ping API on Unix systems TracePlot only returns values for the last hop when run on Linux. Seeing as this effectively prevents usage on Unix-based systems and containers (which is a core goal) the development of TracePlot is halted until the issues are resolved.***

## Frontend
The frontend allows for the creation of new traceroute jobs and viewing the resulting data as box plots (created via plotly.js).


![image](https://user-images.githubusercontent.com/48071390/149257280-c5335bd5-cb08-46f6-8bcf-192376504f90.png)
