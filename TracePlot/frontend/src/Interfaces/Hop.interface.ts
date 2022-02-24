interface Hop {
    hopId: string,
    address: string,
    replyTimes: number[],
    hopNumber: number,
    medianReplyTime: number,
    averageReplyTime: number,
    minimumReplyTime:number,
    maximumReplyTime: number,
    lowerQuartile: number;
    higherQuartile: number,
    parentID: string
}