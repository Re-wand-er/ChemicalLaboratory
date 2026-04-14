import ReagentForecastTable from "./ReagentForecastTable";

/**
 * Формат: таблица с расчётными полями, цветовая индикация срочности, возможно, шкала времени (Gantt) до наступления события.
 */
const Predicts = () => {
  return (
    <>
      <h2>Прогноз</h2>
      <label>Реагент необходимо заказать если остаток меньше мин. колич * 3</label>
      <ReagentForecastTable />
    </>
  );
};
export default Predicts;