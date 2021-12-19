import React, { useState, useEffect } from "react";
import TraceRouteService from "../Services/TraceRouteService";
import BoxPlot from "./BoxPlot";

const TraceRouteCollection = (props) => {
    const [expanded, setExpanded] = useState(false)
    
    const expand = async () => {
        // const hops = await TraceRouteService.getTraceRouteCollection(props.entry["traceRouteCollectionID"])
        setExpanded(!expanded)
        }
    return (<>
            <div className="flex flex-row w-1/2 mt-6">
              <span className="mr-6 w-1/5">{props.entry["targetHostname"]}</span>
              <span className="mr-6 w-1/5">{props.entry["numberOfLoops"]}</span>
            <button className="grow-0 mr-4 w-1/5" onClick={() => expand()}>{expanded ? "Hide details":"View details"}</button>
            </div>
              
            <div>
                {expanded ? <BoxPlot hops={props.entry["hops"]}></BoxPlot>: <></>}
            </div>
            

            </>
            

    )
}

export default TraceRouteCollection