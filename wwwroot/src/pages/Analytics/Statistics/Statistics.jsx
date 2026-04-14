import AverageOperationSize from "./AverageOperationSize";
import CountOfOperations from "./CountOfOperations";
import ReagentsBelowMinimum from "./ReagentsBelowMinimum";
import ReagentsExpire from "./ReagentsExpire";
import ReagentsTurnOverRatio from "./ReagentsTurnOverRatio.jsx"

/**
 * Формат: таблицы, числа, условное форматирование (красный – критично, жёлтый – предупреждение).
 */
const Statistics = () => {
  return (
    <>
      <h2>Статистика</h2>
      <AverageOperationSize />
      <CountOfOperations />
      <ReagentsBelowMinimum />
      <ReagentsExpire />
      <ReagentsTurnOverRatio />
    </>
  );
};
export default Statistics;