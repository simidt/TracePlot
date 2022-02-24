import React from "react";


const MessageDisplay = ({ message }: { message: Partial<MessageDisplayProps> }): JSX.Element => {

    return (
        <>{message === null ?
             null : message.isError ?
              <div className="bg-orange-100 border-l-4 border-orange-500 text-orange-700 p-4"> {message.text}</div>
            : <div className="bg-green-100 border-l-4 border-green-500 text-green-700 p-4"> {message.text}</div>}</>
    )

}

export default MessageDisplay;