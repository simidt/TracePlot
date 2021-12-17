import React, { useState, useEffect, useRef } from "react";
import Plot from 'react-plotly.js';
const BoxPlot = (props) =>{
    const [data, setData] = useState([])
    

    useEffect(()=>{
        const newData = props.hops.reduce((result, hop) => {
            // Filter out hops that did not respond
            if(hop.address !== "N/A"){
                result.push({x:[hop.minimumReplyTime, hop.lowerQuartile, hop.medianReplyTime, hop.higherQuartile, hop.maximumReplyTime],
                    y: "ms",
                    type:"box",
                    name:hop.address
                })
             }
             return result
        }, []);
        // const newData = props.hops.map((hop)=> {
        //     // if(hop.address !== "N/A"){
        //     return {x:[hop.minimumReplyTime, hop.lowerQuartile, hop.medianReplyTime, hop.higherQuartile, hop.maximumReplyTime],
        //             y: "ms",
        //             type:"box",
        //             name:hop.address
        //         }
        //     // }
            
        //     });
        newData.reverse();
        setData(newData)
    },[])


    return ( 
        
    <Plot class="invisible" data={data}
        
        layout={ { title: 'Traceroute', showlegend:false} }></Plot>              
        
    )
}

export default BoxPlot;