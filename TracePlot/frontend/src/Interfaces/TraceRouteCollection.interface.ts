interface TraceRouteCollection{
    traceRouteCollectionID: string,
    targetHostname: string,
    start: Date,
    numberOfLoops: number,
    intervalSize: number,
    hops: Hop[]
}