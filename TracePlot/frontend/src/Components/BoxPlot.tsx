import React, { useState, useEffect} from "react";
import Plot from 'react-plotly.js';


interface Props{
    hops: Hop[]
}
const BoxPlot= ({hops}:Props):JSX.Element =>{
    const [data, setData] = useState<Plotly.Data[]>([])
    

    useEffect(()=>{
        hops.sort((a,b)=> a.hopNumber -b.hopNumber);
        const newData = hops.reduce((result: Plotly.Data[], hop: Hop) => {
            // Filter out hops that did not respond
            if(hop.address !== "N/A"){
                result.push({y:[hop.minimumReplyTime, hop.lowerQuartile, hop.medianReplyTime, hop.higherQuartile, hop.maximumReplyTime],
                    type:"box",
                    name:hop.address
                })
            }

            return result
        }, []);
        
        setData(newData)
    },[hops])

    let layout: Partial<Plotly.Layout> = { 
        title: 'Traceroute',
        showlegend:false,
        plot_bgcolor:'rgba(0,0,0,0)',
        paper_bgcolor:'rgba(0,0,0,0)', 
        xaxis:{
            type: 'category',
            title: 'Hops',
        },
        yaxis: {
            title: "Reply time (ms)"
        }
    }
    return ( 
        
    <Plot className="" data={data}
        
        layout={ layout }></Plot>              
        
    )
}

export default BoxPlot;