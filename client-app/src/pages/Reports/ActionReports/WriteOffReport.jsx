import ReagentOperationReport from "./ReagentOperationReport";

const WriteOffReport = () => {
  return (
    <ReagentOperationReport
      title="Отчет по списаниям за период"
      path="write-off"
    />
  );
}

export default WriteOffReport;