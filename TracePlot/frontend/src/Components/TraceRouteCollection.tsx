import React, { useState } from "react";
import BoxPlot from "./BoxPlot";

const TraceRouteCollection = ({ trc }: { trc: TraceRouteCollection }):JSX.Element => {
    const [expanded, setExpanded] = useState(false)
    const expand = async () => {
        // const hops = await TraceRouteService.getTraceRouteCollection(trc["traceRouteCollectionID"])
        setExpanded(!expanded)
        }
    return (<>
            <div className="flex flex-row w-1/2 mt-6">
              <span className="mr-6 w-1/5">{trc["targetHostname"]}</span>
              <span className="mr-6 w-1/5">{trc["numberOfLoops"]}</span>
              <span className="mr-6 w-1/5">{trc["intervalSize"]}</span>
            <button className="grow-0 mr-4 w-1/5" onClick={() => expand()}>{expanded ? "Hide details":"View details"}</button>
            </div>
              
            <div>
                {expanded ? <BoxPlot hops={trc["hops"]}></BoxPlot>: <></>}
            </div>
            

            </>
            

    )
}

export default TraceRouteCollection