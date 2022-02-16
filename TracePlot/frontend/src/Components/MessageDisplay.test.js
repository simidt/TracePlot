import React from "react";
import "@testing-library/jest-dom/extend-expect";
import { render, screen } from "@testing-library/react";
import MessageDisplay from "./MessageDisplay";

test("Displays a message on success", () => {
  const message = {
    text: "Success",
    isError: false,
  };
  render(<MessageDisplay message={message}></MessageDisplay>);
  const element = screen.getByText("Success");
  expect(element).toBeDefined();
  // Check for correct styling
  expect(element).toHaveClass(
    "bg-green-100 border-l-4 border-green-500 text-green-700 p-4"
  );
});

test("Displays a message on error", () => {
  const message = {
    text: "Error",
    isError: true,
  };
  render(<MessageDisplay message={message}></MessageDisplay>);
  const element = screen.getByText("Error");
  expect(element).toBeDefined();
  // Check for correct styling
  expect(element).toHaveClass(
    "bg-orange-100 border-l-4 border-orange-500 text-orange-700 p-4"
  );
});
