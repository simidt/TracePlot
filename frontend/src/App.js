import "./App.css";
import TraceRouteService from "./Services/TraceRouteService";
import React, { useState, useEffect } from "react";
import TraceRouteCollection from "./Components/TraceRouteCollection";

function App() {
  const [traceRouteEntries, setTraceRouteEntries] = useState([]);
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
    const newObj = { hostname, numberOfIterations, intervalSize };
    const traceRoute = await TraceRouteService.getTraceRoute(newObj);
    setTraceRouteEntries(traceRoute);
  };
  //const handleExpand = async();
  return (
    <div className="h-screen bg-gray-100 p-8">
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
            type="text"
            onChange={(e) => setNumberOfIterations(e.target.value)}
            value={numberOfIterations}
            placeholder="Number of iterations"
          ></input>
          <input
            className=" p-2 mt-4 w-full"
            type="text"
            onChange={(e) => setIntervalSize(e.target.value)}
            value={intervalSize}
            placeholder="Interval size (ms)"
          ></input>
        </div>
        <button className="m-4">TraceRoute</button>
      </form>
      <div className="">
        <span>Target Domain</span>
        <span>Number of Iterations</span>

        <div>
          <div>
            {traceRoutes.map((entry) => (
              <TraceRouteCollection entry={entry}></TraceRouteCollection>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
