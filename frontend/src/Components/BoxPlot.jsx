import React, { useState, useEffect, useRef } from "react";
import Plot from 'react-plotly.js';
const BoxPlot = (props) =>{
    const [data, setData] = useState([])
    

    useEffect(()=>{
        props.hops.sort((a,b)=> a.hopNumber -b.hopNumber);
        const newData = props.hops.reduce((result, hop) => {
            // Filter out hops that did not respond
            if(hop.address !== "N/A"){
                result.push({y:[hop.minimumReplyTime, hop.lowerQuartile, hop.medianReplyTime, hop.higherQuartile, hop.maximumReplyTime],
                    x: "ms",
                    type:"box",
                    name:hop.address
                })
             }
             return result
        }, []);
        
        setData(newData)
    },[])

    let layout = { 
        title: 'Traceroute',
        showlegend:false,
        plot_bgcolor:'rgba(0,0,0,0)',
        paper_bgcolor:'rgba(0,0,0,0)', 
        xaxis:{
            type: 'category',
            title: 'Hops',
        },
    }
    return ( 
        
    <Plot class="invisible" data={data}
        
        layout={ layout }></Plot>              
        
    )
}

export default BoxPlot;