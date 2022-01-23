import "./App.css";
import TraceRouteService from "./Services/TraceRouteService";
import React, { useState, useEffect } from "react";
import TraceRouteCollection from "./Components/TraceRouteCollection";

function App() {
  const [hostname, setHostname] = useState("");
  const [numberOfIterations, setNumberOfIterations] = useState("");
  const [intervalSize, setIntervalSize] = useState("");
  const [traceRoutes, setTraceRoutes] = useState([]);

  useEffect(() => {
    TraceRouteService.getTraceRoutes().then((response) => {
      setTraceRoutes(response);
    });
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const newObj = {
      hostname,
      numberOfIterations: parseInt(numberOfIterations, 10),
      intervalSize: parseInt(intervalSize, 10),
    };

    const response = await TraceRouteService.postTraceRoute(newObj);
    if (response.status === 200) {
      setMessage({
        text: response.data.response,
        isError: false,
      });
      //If the statuscode is not 200, an error has occurred in the backend
    } else {
      setMessage({
        text: response.data.response,
        isError: true,
      });
    }
    //Only show the confirmation for 5 seconds
    setTimeout(() => setMessage(null), 5000);

  };
  return (
    <div className="min-h-screen bg-gray-100 p-8">
      <form onSubmit={handleSubmit} className="flex">
        <div className="w-1/5  flex-col justify-center ">
          <input
            className="p-2 mt-4 w-full "
            type="text"
            onChange={(e) => setHostname(e.target.value)}
            value={hostname}
            placeholder="Target hostname"
          ></input>
          <input
            className=" p-2 mt-4 w-full"
            onChange={(e) =>
              setNumberOfIterations(e.target.value.replace(/\D/, ""))
            }
            value={numberOfIterations}
            placeholder="Number of iterations"
          ></input>
          <input
            className=" p-2 mt-4 w-full"
            onChange={(e) => setIntervalSize(e.target.value.replace(/\D/, ""))}
            value={intervalSize}
            placeholder="Interval size (ms)"
          ></input>
        </div>
        <button className="h-1/2 m-6 mt-32 bg-blue-400" type="submit">
          Start Traceroute
        </button>
      </form>
      <h1>Completed Traceroutes</h1>
      <div className="flex flex-row w-1/2">
        <span className="mr-6 w-1/5">Target Domain</span>
        <span className=" w-1/5">Number of Iterations</span>
        <span className=" w-1/5">Interval Size (ms)</span>
      </div>

      {traceRoutes.map((entry) => (
        <TraceRouteCollection
          key={entry["traceRouteCollectionID"]}
          entry={entry}
        ></TraceRouteCollection>
      ))}
    </div>
  );
}

export default App;
