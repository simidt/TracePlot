import React, { useState, useEffect } from "react";
import TraceRouteService from "../Services/TraceRouteService";
import BoxPlot from "./BoxPlot";

const TraceRouteCollection = (props) => {
    const [expanded, setExpanded] = useState(false)
    
    const expand = async () => {
        // const hops = await TraceRouteService.getTraceRouteCollection(props.entry["traceRouteCollectionID"])
        setExpanded(!expanded)
        }
    return (
        <div key={props.entry["targetHostname"]}>
              <span>{props.entry["targetHostname"]}</span>
              <span>{props.entry["numberOfLoops"]}</span>
              <button onClick={() => expand()}>{expanded ? "Hide details":"View details"}</button>
            <div>
                {expanded ? <BoxPlot hops={props.entry["hops"]}></BoxPlot>: <></>}
            </div>
            </div>
            

    )
}

export default TraceRouteCollection