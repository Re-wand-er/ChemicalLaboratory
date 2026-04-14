import CategoryPie from './CategoryPie.jsx'
import ReagentsBarChart from './ReagentsBarChart.jsx';
import ReagentsLineChart from './ReagentsLineChart.jsx';
import ReagentsPie from './ReagentsPie.jsx';

const Graphics = () => {
  return (
    <>
      <h2>Графика</h2>
      <CategoryPie />
      <ReagentsBarChart />
      <ReagentsLineChart />
      <ReagentsPie />
    </>
  );
};
export default Graphics;