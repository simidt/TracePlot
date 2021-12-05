import logo from "./logo.svg";
import "./App.css";
import TraceRouteService from "./Services/TraceRouteService";
import React, { useState, useEffect } from "react";

function App() {
  const [traceRouteEntries, setTraceRouteEntries] = useState([]);
  const [hostname, setHostname] = useState("");
  const [traceRoutes, setTraceRoutes] = useState([]);
  useEffect(() => {
    TraceRouteService.getTraceRoute().then((response) => {
      console.log(response);
      setTraceRoutes(response);
    });
  }, []);
  const handleSubmit = async (e) => {
    e.preventDefault();
    const traceRoute = await TraceRouteService.getTraceRoute(hostname);
    setTraceRouteEntries(traceRoute);
  };
  const handleExpand = async ()
  return (
    <div className="App">
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          onChange={(e) => setHostname(e.target.value)}
          value={hostname}
        ></input>
        <button>TraceRoute</button>
      </form>
      <table>
        <thead>
          <tr>
            <th>Target Domain</th>
            <th>Number of Iterations</th>
            <th> </th>
          </tr>
        </thead>
        <tbody>
          {traceRoutes.map((entry) => (
            <tr key={entry["targetHostname"]}>
              <td>{entry["targetHostname"]}</td>
              <td>{entry["numberOfLoops"]}</td>
              <button onClick={() => {}}>Expand</button>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default App;
