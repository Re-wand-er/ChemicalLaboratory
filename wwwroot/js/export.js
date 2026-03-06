// { // Экспорт/импорт
// // let znach = 5;
// // let kol = 10;
// // // Виды экспорта
// // export default function myAlert(message = "Default message"){console.log(message);} 
// // export let exportVariable = "Export Variable";
// // export {znach, kol}
// }
let result = 1;
let intervalID = null;

// Слушаем сообщения от главного потока
self.onmessage = (e) => {
    if (e.data === "start") {
        // Запускаем интервал
        intervalID = setInterval(() => {
            result = result * 2;
            console.log("Worker: result =", result);
            
            // Отправляем результат главному потоку
            self.postMessage({ result, status: "running" });
            
            if (result >= 32) {
                clearInterval(intervalID);
                self.postMessage({ result, status: "finished" });
            }
        }, 1000);
    }
    
    if (e.data === "stop") {
        clearInterval(intervalID);
        self.postMessage({ status: "stopped" });
    }
};
