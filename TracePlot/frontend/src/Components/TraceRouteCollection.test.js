import React from "react";
import "@testing-library/jest-dom/extend-expect";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import TraceRouteCollection from "./TraceRouteCollection";

const testData = {
  traceRouteCollectionID: "039b738c-3c9f-4ec9-8af3-6b43add201fc",
  targetHostname: "www.quartz-scheduler.net",
  start: "2022-01-20T23:42:10.7262101",
  numberOfLoops: 1,
  intervalSize: 2500,
  hops: [
    {
      hopId: "483fab93-37d2-449f-8bcb-7a171e8c2d17",
      address: "62.155.243.137",
      replyTimes: [],
      hopNumber: 2,
      medianReplyTime: 0,
      averageReplyTime: 7,
      minimumReplyTime: 7,
      maximumReplyTime: 7,
      lowerQuartile: 0,
      higherQuartile: 0,
      parentID: "039b738c-3c9f-4ec9-8af3-6b43add201fc",
    },
    {
      hopId: "4a7c49a9-2061-4d66-9664-8623c85eff7b",
      address: "195.219.148.122",
      replyTimes: [],
      hopNumber: 7,
      medianReplyTime: 0,
      averageReplyTime: 14,
      minimumReplyTime: 14,
      maximumReplyTime: 14,
      lowerQuartile: 0,
      higherQuartile: 0,
      parentID: "039b738c-3c9f-4ec9-8af3-6b43add201fc",
    },
    {
      hopId: "6526eaa9-adf9-4278-bf9c-265df1ab97df",
      address: "195.219.50.174",
      replyTimes: [],
      hopNumber: 6,
      medianReplyTime: 0,
      averageReplyTime: 21,
      minimumReplyTime: 21,
      maximumReplyTime: 21,
      lowerQuartile: 0,
      higherQuartile: 0,
      parentID: "039b738c-3c9f-4ec9-8af3-6b43add201fc",
    },
    {
      hopId: "8d5a273d-00e4-41dd-ac65-04061bdf4b1a",
      address: "217.5.112.210",
      replyTimes: [],
      hopNumber: 3,
      medianReplyTime: 0,
      averageReplyTime: 13,
      minimumReplyTime: 13,
      maximumReplyTime: 13,
      lowerQuartile: 0,
      higherQuartile: 0,
      parentID: "039b738c-3c9f-4ec9-8af3-6b43add201fc",
    },
    {
      hopId: "bd303704-1f56-4aee-bbea-9acb87c353de",
      address: "195.219.219.72",
      replyTimes: [],
      hopNumber: 5,
      medianReplyTime: 0,
      averageReplyTime: 27,
      minimumReplyTime: 27,
      maximumReplyTime: 27,
      lowerQuartile: 0,
      higherQuartile: 0,
      parentID: "039b738c-3c9f-4ec9-8af3-6b43add201fc",
    },
    {
      hopId: "c0b8ef05-3261-48c8-8342-c2b4f604abc6",
      address: "10.15.0.1",
      replyTimes: [],
      hopNumber: 1,
      medianReplyTime: 0,
      averageReplyTime: 32,
      minimumReplyTime: 32,
      maximumReplyTime: 32,
      lowerQuartile: 0,
      higherQuartile: 0,
      parentID: "039b738c-3c9f-4ec9-8af3-6b43add201fc",
    },
    {
      hopId: "c3ace5dc-7d55-4ceb-96d7-71bb4b7b8f17",
      address: "104.21.37.149",
      replyTimes: [],
      hopNumber: 8,
      medianReplyTime: 0,
      averageReplyTime: 13,
      minimumReplyTime: 13,
      maximumReplyTime: 13,
      lowerQuartile: 0,
      higherQuartile: 0,
      parentID: "039b738c-3c9f-4ec9-8af3-6b43add201fc",
    },
    {
      hopId: "c8cd6ed0-5f40-4703-9a98-4e1247f8e9ab",
      address: "80.156.162.178",
      replyTimes: [],
      hopNumber: 4,
      medianReplyTime: 0,
      averageReplyTime: 26,
      minimumReplyTime: 26,
      maximumReplyTime: 26,
      lowerQuartile: 0,
      higherQuartile: 0,
      parentID: "039b738c-3c9f-4ec9-8af3-6b43add201fc",
    },
  ],
};

window.URL.createObjectURL = jest.fn();
test("Toggle works", () => {
  const { container } = render(
    <TraceRouteCollection entry={testData}></TraceRouteCollection>
  );
  const button = screen.getByText("View details");
  userEvent.click(button);

  const div = container.querySelector(".togglableContent");
  expect(div).toBeDefined();
});

test("Values are displayed correctly", () => {
  const { container } = render(
    <TraceRouteCollection entry={testData}></TraceRouteCollection>
  );
  const hostname = screen.getByText(testData.targetHostname);
  const numberOfIterations = screen.getByText(testData.numberOfLoops);
  const intervalSize = screen.getByText(testData.intervalSize);

  expect(hostname).toBeDefined();
  expect(numberOfIterations).toBeDefined();
  expect(intervalSize).toBeDefined();
});
