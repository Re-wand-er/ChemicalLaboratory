////////////////////////////////////////////////////////////////////////
{ // Работа с переменной как с массивом и объектом
// var user = [["Tom", 14],
//             ["Sam", 16]]

// function createObject(object) 
// {
//     let res = Object.fromEntries(object);
//     // for(const [name, age] of object)
//     // {
//     //    res[name] = age;
//     // }
    
//     res.print = () =>
//     {
//         for(const prop in res)
//             if(typeof res[prop] !== "function") 
//                 console.log(`Свойство - ${prop} : Значение - ${res[prop]} ${this}`);
//     }

//     return res;
// }

// var result = createObject(user);
// result.print();
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Функция и св-ва переменной как объекта
// function getSalary(status){
//     if(status==="senior") return 1500;
//     else return 500;
// }

// const name = "Tom";
// const age = 37;
// const sex = "M";
// const person = { name, age, sex, salary: getSalary()};
 
// console.log(person);    // {name: "Tom", age: 37, salary: 500}
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Итерирование подсв-ва в виде массива объектов объекта
// const country = {
 
//     name: "Швейцария",
//     languages: ["немецкий", "французский", "итальянский"],
//     capital:{
     
//         name: "Берн",
//         population: 126598
//     },
//     cities: [
//         { name: "Цюрих", population: 378884},
//         { name: "Женева", population: 188634},
//         { name: "Базель", population: 164937}
//     ]
// };

// // для работы с массивами можно по ключу, с объектами по именам свойств
// for(const tempObject of country.cities) // итерация по массиву cities (for..of)
// {
//     for(const tmp in tempObject) // итерация по свойствам отдельного объекта (for..in)
//     {
//         console.log(`${tmp} ${tempObject[tmp]}`)
//     }
// }
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Различные виды поверхностного копирования
// const tom = { 
//     name: "Tom", 
//     age: 37,
//     job: {
//         companyName: "Microsoft"
//     }
// };

// const bob = Object.assign({}, tom); // поверхностное копирование
// let sam = {};                       
// Object.assign(sam, tom);            // поверхностное копирование (аналогично bob)
// const vas = {...tom};               // поверхностное копирование (аналогично bob)
// const res = structuredClone(tom);   // полное копирование 

// bob.name = "Bob"; 
// bob.age = 41;
// bob.job.companyName = "У всех поменялось";

// sam.name = "Sam";
// sam.age = 31;
         
// vas.name = "Vas";
// vas.age = 21;

// res.job.companyName = "Google";

// console.log(`Объект tom. Name: ${tom.name}   Age: ${tom.age}   ${tom.job.companyName}`); // Объект tom. Name: Tom   Age: 37   У всех поменялось
// console.log(`Объект bob. Name: ${bob.name}   Age: ${bob.age}   ${bob.job.companyName}`); // Объект bob. Name: Bob   Age: 41   У всех поменялось
// console.log(`Объект sam. Name: ${sam.name}   Age: ${sam.age}   ${sam.job.companyName}`); // Объект sam. Name: Sam   Age: 31   У всех поменялось
// console.log(`Объект vas. Name: ${vas.name}   Age: ${vas.age}   ${vas.job.companyName}`); // Объект vas. Name: Vas   Age: 21   У всех поменялось
// console.log(`Объект res. Name: ${res.name}   Age: ${res.age}   ${res.job.companyName}`); // Объект res. Name: Tom   Age: 37   Google
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Сравнение типа объекта по цепочке прототипов и получение св-в через Object.keys(object)
// function Point2D(x, y)
// {
//     this.X = x;
//     this.Y = y;
//     this.log = function(){console.log(this.X, this.Y)};
// }
// //console.log(Point2D.prototype);

// function Point3D(x, y, z)
// {
//     this.X = x;
//     this.Y = y;
//     this.Z = z;
//     this.log = function(){console.log(this.X, this.Y, this.Z)};
// }

// const local2DPoint = new Point2D(1, 2); //console.log(local2DPoint.constructor);
// const local3DPoint = new Point3D(1, 2, 3);

// local2DPoint.log();
// local3DPoint.log();

// for(const obj of Object.keys(local2DPoint)){console.log(local2DPoint instanceof Point2D);console.log(local2DPoint instanceof Point3D);console.log(`${obj}`);}
// for(const obj of Object.keys(local3DPoint)){console.log(local3DPoint instanceof Point2D);console.log(local3DPoint instanceof Point3D);console.log(`${obj}`);}
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Вызов методов через function.call(object, {св-ва})/function.apply(object, [св-ва])
// const sqrt = new Function("", "console.log(this.name);");

// //  function.call - позволяет обратиться к объекту переданному в метод call на первой позиции
// sqrt.call({name: "First try"}); // First try
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ //asfdasdf
// const people = Object.create(Object.prototype, {
//     name: {
//         value: "Tom"
//     },
//     _age:{
//         value: 19,
//         writable: false
//     },
//     age:
//     {
//         enumerable: false,
//         set() {console.log(`Изменение запрещено`)},
//         get() {return this._age;}
//     }
// });

// // people.age;         // Текущее значение возраста: 19
// // people.age = "New"; // Изменение запрещено
// // people.age;         // Текущее значение возраста: 19

// const {name, age} = people;
// console.log(name, age)

// const mainPeople = [
//     {name: "Tom", age: 34},
//     {name: "Bob", age: 23},
//     {name: "Sam", age: 32}
// ];

// for(let {name: username, age: userage} of mainPeople){
//     console.log(`Name: ${username}  Age: ${userage}`);
// }
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Класса/Наследование и особенности геттеров/сеттеров и приватных полей и т.д.
// class Person{

//     // эти свойства можно и не писать т.к. определены в конструкторе //
//     //name; // не должно быть объявления поля и сеттер с одним именем (тогда объявление перекроет сеттер)
//     age;

//     // Приватное св-во (обязательно к объявлению)
//     #mainName;
//     static Login = "Log"; // статическое поле

//     constructor(name, age)
//     {
//         this.name = name; 
//         this.age = age;
//     }

//     set name(value){
//         this.#mainName = value;
//         console.log(`Установка значение имени = ${this.#mainName}; Его логин = ${Person.Login}`);
//     }

//     get name(){
//         return this.#mainName;
//     }

//     move(){return this.name +" move";}
// }

// class Employee extends Person
// {
//     constructor(name, age){super(name, age);}

//     move()
//     {
//         console.log("Employee " + super.move());
//     }
// }
// const emp = new Employee("Bob", 21);

// // Не имеет смысла
// const User = class Person{ } // User вообще отдельная сущность никак не связанная с Person

// //const us = new User("Sam", 18);
// const us = new Person("Sam", 18);
// us.name = "Tom";
// us.weight = 88; // Объекту класса также можно добавлять доп. свойства

// // Т.к. наследование "обертка" над цепочкой прототипов, то возможно
// console.log(emp instanceof Employee); // true
// console.log(emp instanceof Person); // also true
// emp.move();

// console.log(us.name, us.age, us.weight);
// //us.move(); // us.move is not a function
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Основы работы с массивом (фишки работы с ним)
// const name = [];
// const nameArray = new Array("Sam", "Bob");
// const nameArrayOf = Array.of("Denny", "Jake");
// const newNameArrayPlusOf = [...nameArray, "Jankins", ...nameArrayOf]; // объединение массивов

// // ...<имя массива> в целом служит для разложения массива на значения 
// function information(firstArgument, secondArgument) 
// {
//     console.log("Massive argument", firstArgument, secondArgument);
// }

// // information(nameArray); - 2 разных случая. В этом случае firstArgument - nameArray, secondArgument - undefind
// information(...nameArray); // Massive argument Sam Bob

// // массив состоящий из отдельных букв
// const arrayOfLetter = Array.from("Array divided on letter");

// name[1] = "Tom";
// console.log(name[0]); // undefined    
// console.log(arrayOfLetter);
// console.log(nameArray); // ['Sam', 'Bob'] - получим описание массива
// console.log(...nameArray); // Sam Bob - просто разложение массива на его значения
// console.log(...newNameArrayPlusOf); // массивы nameArray и nameArrayOf + "Jankins"

// // Также нужно помнить что при копировании объектов копируется их ссылка, что влияет на все объекты с этой же ссылкой
// const people = [{name:"Sam"}, {name:"Tom"}, {name:"Bob"}];
// const employees = [...people];

// // Если менять значение объекта, то оно сохранится и у другого указателя
// employees[0].name = "Dan";
// console.log(...employees);     //  [{name:"Dan"}, {name:"Tom"}, {name:"Bob"}]
// console.log(...people);        //  [{name:"Dan"}, {name:"Tom"}, {name:"Bob"}]

// // Возвращение значения (теперь снова оба объекта ссылаются на новое значение "Sam", хоть и было оно изменено только в employee)
// employees[0].name = "Sam";

// // Однако если поменять не значение объекта, а сам объект
// employees[0] = {name: "Dan"};
// console.log(...employees);     //  [{name:"Dan"}, {name:"Tom"}, {name:"Bob"}]
// console.log(...people);        //  [{name:"Sam"}, {name:"Tom"}, {name:"Bob"}] 

// // Метода для работы с массивами можнопосмотреть в интернете (metanit)

// class Team extends Array
// {
//     constructor(teamName, ...members)
//     {
//         super(...members); // обязательно перед this
//         this.teamName = teamName;
//     }

//     push(memberName) // Можно переопределять методы родителя
//     {
//         if(!memberName) {console.log("Ошибка добавления"); return;}
//         super.push(memberName);
//     }
// }

// const barcelona = new Team("Barcelona", "Tom", "Sam", "Denny");

// for (const mem of barcelona) // перебор строго по элементам массива ("Barcelona - не является элементом массива")
//     console.log(mem); // Tom Sam Denny


// barcelona.push(); // Ошибка добавления
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Интерполяция и регулярные выражения
// const str = "Hello";
// const str2 = String("Hello2");

// const tom = {name: "Tom", age: 19}
// const names = ["Sam", "Bob", "Taylor"];

// const mark = 
// `
// <div>
//     <h>Name: ${tom.name}</h>
//     <h1>Age: ${tom.age}</h1>
//     <ul>
//     ${names.map(name => `<li>${name}</li>`)}
//     </ul>
// </div>
// `
// document.body.innerHTML = mark;

// function proverka(array, value1){
//     console.log(array[0],value1, array[1]);
// }
// proverka`Ну и херня ${mark} этот ваш html`; // используется для контроля интерполяции (sql, css, html)

// // Регулярные выражения
// const regexp = /regular/; // ищет одно вхождение
// const regexp2 = new RegExp(/regular2/);

// const experimString = "regularNO day of regularKA using";
// const res = experimString.match(regexp);
// console.log(res);

// // Поиск всех вхождений в строке 
// const regexpg = /regular[a-zA-Z]*/g; // благодаря флагу g ищет все вхождения
// while((newRes = regexpg.exec(experimString)) !== null)
//     console.log("Текущее значение res: " + newRes[0] + " и его индекс: " + newRes["index"]);

// console.log(experimString.match(regexpg));

}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Обработка ошибок
// try
// {
//     asdaf(); // Alarm
//     var a = 5 / 0; // Not Alarm :(   
// }
// catch(err)
// {
//     console.log(err); //ReferenceError: asdaf is not defined
//     console.log("Alarm");
// }
// finally
// {
//     console.log("Conclusion");
// }
// console.log(a); // undefined

// function exception(arg)
// {
//     if (arg !== 0) throw "Ебать!, а это не ноль";
// }

// // Вызывает ошибку и останавливает работу программы
// //exception(5); // Uncaught Ебать!, а это не ноль

// try{exception(5);}
// catch(err){console.log(err);} //Ебать!, а это не ноль

// // Можно создавать свои ошибки
// class MyError extends Error
// {
//     constructor(value, ...params)
//     {
//         super(...params);
//         this.myInclusionError = value;
//     }
// }

// try{throw new MyError("Моя инклюзивная ошибка", "Ошибка и что с того?");}
// catch(error){   

//     //Получение типа ошибок возможно таким образом
//     if (error instanceof TypeError) {
//         console.log("Некорректный тип данных.");
//     } else if (error instanceof RangeError) {
//         console.log("Недопустимое значение");
//     }
//     else if (error instanceof MyError){
//         console.log(`${error.myInclusionError}`); // мое свойство
//     }
//     console.log(error.message); // свойство класса Error
// }

// function A(){
//     console.log("func A starts");
//     callSomeFunc();
//     console.log("func A ends");
// }
// function B(){
//     console.log("func B starts");
//     try{
//         A();
//     }
//     catch{
//         throw new Error();
//         //console.log("Error B occured");
//     }
//     console.log("func B ends");
// }
// function C(){
//     console.log("func C starts");
//     try{
//         B();
//     }
//     catch{
//         console.log("Error C occured");
//     }
//     console.log("func C ends");
// }
 
// C();
// console.log("program ends");

}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Symbol и Proxy
// const id = Symbol();
// const id2 = Symbol();
// console.log(id == id2);

// const tom = {name: "Tom", age: 19};
// const handler = 
// {
//     get: function(target, prop){
//         if(prop == "age") return 9999; 
//         else return target[prop];
//     },

//     set: function(target, prop, value){
//         if(prop == "age"){ target.age = -12; return true;}
//         else {target[prop] = value;}
//     }
// }

// const proxy = new Proxy(tom, handler);
// console.log(proxy.age); // 9999
// proxy.age = 20;
// console.log(tom.age); // -12
// proxy.height = 185;
// console.log(proxy.height); // 185
// console.log(tom.height); // 185

// const user = {
//     name: "user",
//     get info(){return "User: " + this.name;},
//     set info(value){this.name = "UserValue: " + value}
// }
// console.log(user.info); // User: user
// user.info = "Dima"
// console.log(user.info); // User: UserValue: Dima

// const usern = {
//   name: 'Tom',
//   get info() {
//     return "adf " + this.name;
//   }
// };

// const proxyn = new Proxy(usern, {
//   get(target, prop) {
//     return target[prop]; 
//   }
// });

// console.log(proxyn.info);


// const base = {
//   get info() {
//     return this.name;
//   }
// };
// const usern2 = {
//     name: "usern2"
// }

// Object.setPrototypeOf(usern2, base);

// const proxyn2 = new Proxy(usern2, {
//   get(target, prop) {
//     return target[prop]; // Для корректности работы советуют receiver: return Reflect.get(target, prop, receiver);
//   }
// });

// console.log(proxyn2.info); 
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Генераторы, структура данных
// Генераторы
// function *newEnumerator(){
//     yield "First value";
//     yield "Second value";
//     yield "Third value";
// }

// const enumer = newEnumerator();
// console.log(enumer.next()); // {value: 'First value', done: false}
// console.log(enumer.next()); // {value: 'Second value', done: false}
// enumer.return();            // Принудительно завершает работу генератора newEnumerator
// console.log(enumer.next()); // {value: undefined, done: true}


// function* getNumber(){
//     const n = yield 5;
//     console.log("n:", n);
//     const m = yield 5 * n;
//     console.log("m:", m);
//     yield 5 * m;
// }
// const enu = getNumber();

// console.log(enu.next(1).value); // 5
// console.log(enu.next(2).value); // 10 n: 2
// console.log(enu.next(3).value); // 15 m: 3

// function* generator()
// {
//     try{
//         yield 5;
//         yield 10;
//         yield 15;
//     }
//     catch(err){
//         console.log("MyError:", err);
//     }
// }
// const res = generator();

// console.log(res.next()); // {value: 5, done: false}
// res.throw("New Exception"); // MyError: New Exception
// console.log(res.next()); // {value: undefined, done: true}

// // Set - служит для формирования множеств с уникальными значениями
// const arrayOfNumber = [1, 1, 2, 3, 4, 4, 3, 10, 9, 5.26];
// const setArray = new Set(arrayOfNumber); // Только уникальные значения
// console.log(setArray); // Set(7) {1, 2, 3, 4, 10, …}
// const arr = Array.from(setArray); //преобразование обратно в массив
// console.log(arr); // [1, 2, 3, 4, 10, 9, 5.26]
// // Также есть WeakSet только его значения это объекты. Также WeakSet привязана к элементам по ссылке, 
// // поэтому если как-то будет ссылка изменена, то и объект измениться или исчезнет

// // Map - словарь ключ-значение
// const map = new Map([["first", "первый"], ["second", "второй"], ["third", "третий"]]);
// for(const m of map){console.log(m[0],m[1]);} // first первый ...
// for(const m of map.keys()){console.log(m);} // first ...
// for(const m of map.values()){console.log(m);} // первый ...
// WeakMap - все значения объекты. Не поддерживает перебор. Также имеет слабые ссылки как WeakSet
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Работа с HTML и DOM-деревом
// console.log(document.lastModified);  
// console.log(document.title); 
// console.log(document.URL);   

// for(const image of document.images){console.log(image.src, image.alt);}

// console.log(document.body);
// console.log(document.querySelector("script").src);
// document.querySelector("script").src = "js/layout.js";
// console.log(document.querySelector("script").src);

// // const script = document.createElement("script"); // создание нового элемента
// // script.src = "js/layout.js"; // его путь
// // script.defer = true; // ждет сначала полную загрузку html, а потом выполняет код js 
// // document.body.appendChild(script); // позволяет загружать новые скрипты

// {
// // Такой вариант получения под тегов
// const p = document.querySelector("div");
// const p1 = p?.querySelectorAll("p");
// if (p1)
//     for(const p2 of p1) console.log(p2);

// // Или можно так
// document.querySelectorAll("div p").forEach(p => console.log(p));
// }

// const secondNode = document.getElementById("secondId");
// console.log(secondNode.firstElementChild.firstChild); // Что-то
// console.log(secondNode.parentElement); // <h1>

// for(elem of secondNode.children){console.log(elem);} // все листья

// console.log(secondNode.childElementCount); // 2 (эквивалентно node.children.lenght)
// console.log(secondNode.firstElementChild.firstChild.nodeValue); // Что-то (эквивалентно node.firstElementChild.innerText)

// const header = document.getElementById("firstId");
// console.log(Object.getPrototypeOf(header)); // HTMLHeadingElement
// console.log(header.innerText); // Значение номер 1

// header.textContent = "Новое Значение номер 2";
// console.log(header.innerText); // Новое Значение номер 2

// header.innerHTML = "<div><span style='color:red;'>Изменение новых номеров</span></div>"; // Изменение внутреннего html на красный цвет

// const elementA = document.createElement("a");
// elementA.innerText = "Созданный в коде элемент"; // добавляет внутренний текст
// secondNode.appendChild(document.createElement("br")); // добавляет узел-элемент на страницу
// secondNode.insertBefore(elementA, secondNode.firstChild); // добавляет узел перед первым элементом

// // cloneNode - копирует простой узел (<p>, <h1> и т.д.)
// // replaceChild - заменяет старый простой узел новым

// secondNode.removeChild(secondNode.firstElementChild); // удаляет первый элемент - "Созданный в коде элемент"

// console.log(secondNode.id); // secondId (аналогично second.getAttribute("id"))

// // getAttribute("название атрибута") - получает значение атрибута
// // createAttrbute("название атрибута") - создает атрибут
// // setAttribute("название атрибута", "значение атрибута") - устанавливает значение атрибута или создает его если его не было
// // removeAttribute("название атрибута") - удаление атрибута

// console.log(header.firstElementChild.firstElementChild.style.color) // red (Header - div - span - style - color)
// header.firstElementChild.className = "firstClass"; // добавляет название класса для div
// console.log(header.firstElementChild.className); // firstClass
// // classList позволяет добовлять много класса через add, remove, toggle

// // Есть возможность создавать собственные html-теги
// class MyHtmlTeg extends HTMLElement{
//     constructor(){
//         super();
//         this.innerText = "Собственный тег! А кто еще так может?";
//     }

//     showTime(){alert(new Date().toTimeString());}
// }

// customElements.define("my-html-teg", MyHtmlTeg); 

// const myTeg = document.getElementById("myHtmlTeg");
// myTeg.addEventListener("click",()=>myTeg.showTime()); // отображает время!!!
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // События, их вызов, свои события
// const func = () => alert("firstId");

// const first = document.getElementById("firstId");
// first.addEventListener("dblclick", func); // Для действия обязателна функция. Есть 3-ий параметр он показывает будет событие восходящим или нисходящим
// //document.getElementById("firstId").onclick = ()=>alert("fitstId"); // можно и так 

// // Если вместо func была лямбда функция, то удаление не сработало бы, т.к. это 2 разных объекта
// // Даже если не совпадает 3-й параметр, то удаление не произойдет
// document.getElementById("firstId").removeEventListener("dblclick", func); 

// var counter = 0;

// const butt = document.getElementById("btn");
// butt.addEventListener("click", ()=>{butt.textContent = `Пустое значение : ${++counter}`;});

// // onclick="return a_click(this)" - переадресации не будет
// function a_click(anchor, ev){  // anchor -> this, ev -> event
//     console.log(anchor.href); // anchor - сам элемент
//     console.log(ev.type); // ev - событие Event
//     ev.preventDefault(); // аналогично не дает переадресацию
//     console.log(anchor === ev.target); // true

//     ev.stopPropagation(); // останавливает выполнение текущего обработчика, остальные обработчики продолжат работу
//     ev.stopImmediatePropagation(); // останавливает работу всех остальных обработчиков
//     //return false;
// }

// const redRect = document.getElementById("redRect");
// redRect.addEventListener("click", function(){
//     console.log("Событие на redRect");
// }, true); // нисходящее событие
 
// const blueRect = document.getElementById("blueRect");
// blueRect.addEventListener("click", function(){
//     console.log("Событие на blueRect");
// }, {capture: true}); // {capture: true} - нисходящее событие
 
// document.body.addEventListener("click", function(){
//     console.log("Событие на body");
// }, false); // восходящее событие

// function setColor(event){
//     event.stopImmediatePropagation();

//     if(event.type === "mouseenter")
//         event.target.style.backgroundColor = "red";
//     else if(event.type === "mouseleave")
//         event.target.style.backgroundColor = "blue";

//     console.log(event.screenX,event.screenY); // положение курсора относительно монитора при возникновении события
// }
// blueRect.addEventListener("mouseenter", setColor); // mouseenter - для собственного элемента, mouseover - для собственного и внетренних
// blueRect.addEventListener("mouseleave", setColor); // mouseleave - для собственного элемента, mouseout - для собственного и внетренних

// const blueRectStyle = window.getComputedStyle(blueRect);

// window.addEventListener("keydown", move); // Квадрат двигается

// function move(e){
//     let left = parseInt(blueRectStyle.marginLeft);
//     let top = parseInt(blueRectStyle.marginTop);

//     switch(e.key){
//         case "ArrowLeft":
//             if(left > 0)
//                 blueRect.style.marginLeft = left - 10 + "px";
//             break;
//         case "ArrowUp":
//             if(top > 0)
//                 blueRect.style.marginTop = top - 10 + "px";
//             break;
//         case "ArrowRight":
//             if(left < document.documentElement.clientWidth - 200)
//                 blueRect.style.marginLeft = left + 10 + "px";
//             break;
//         case "ArrowDown":
//             if(left < document.documentElement.clientHeight - 200)
//                 blueRect.style.marginTop = top + 10 + "px";
//             break;              
//     }
// }

// const event = new MouseEvent("mouseenter"); // Создается объект события
// blueRect.dispatchEvent(event); // Вызывается заранее добавленное событие

// redRect.dispatchEvent(new MouseEvent("click")); // Вызывается конкретное событие в любом месте кода

// // Собственное событие 
// // 1 вариант
// function Account(money){
//     let _money = money;
//     this.pay=function(sum){
//         if(_money > sum)
//             _money -= sum;
//         console.log("Summ", _money);

//         document.dispatchEvent(new Event("payment"));
//     }
// }
// const pay = new Account(150);

// butt.addEventListener("click", ()=>pay.pay(25));
// document.addEventListener("payment", ()=>console.log("Payment succeeded!")); // Определение события, оно может быть определено везде
// /////////

// // 2 вариант
// // Собственное событие со своими свойствами
// function NewAccount(money){
//     let _money = money;
//     this.pay=function(sum){
//         const data = {
//             payment: sum,
//             balance: _money    
//         };
//         if(_money => sum){
//             _money -= sum;
//             console.log(_money);

//             document.dispatchEvent(new CustomEvent("success",{ detail: data }));
//         }
//         else{
//             document.dispatchEvent(new CustomEvent("failed",{ detail: data }));
//         }
//     }
// }
// const acc = new NewAccount(200);

// butt.addEventListener("click", ()=>acc.pay(50));
// document.addEventListener("success", (e)=>console.log("Success",e.detail.payment, "Balance",e.detail.balance));
// document.addEventListener("failed", (e)=>console.error("Failed Summ",e.detail.payment, "Failed Balance",e.detail.balance));
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Работа с формами и ВАЛИДАЦИЯ и Validation API 
    // document.<название формы>.<элемент>
    // методы:
    // submit() - отправка данных на сервер
    // reset() - очистка полей
    // <элемент>.focus() - фокус на элемент
    // blur() - убирает фокус с элемента

    // function sendForm(e){
    //     if(document.registrationForm.password.value.length < 8) {
    //         console.error("Пароль слишком коротк длина");
    //         document.registrationForm.password.style = "color: red";
    //         e.preventDefault();
    //     }
    // }

    // const subm = document.registrationForm.button;
    // subm.addEventListener("click", sendForm);

    // const login = document.getElementById("login");
    // login.addEventListener("blur", ()=>console.log("Ну куда уходишь дорогой?"));

    // //
    // const printBlock = document.getElementById("printBlock");
    // const myForm = document.myForm;
    // function onclick(e){
    //     if (e.target.name === "languages")
    //         printBlock.textContent = `Вы выбрали: ${e.target.value}`;
    // }
    // myForm.addEventListener("change", onclick);

    // //
    // function selectChanges(e){
    //     if(e.target.tagName === "SELECT") // В SELECT (обязательно большими) - возникает событие 
    //         console.log(e.target.value);        // Не в Option
    // }

    // const selecter = document.getElementById("SelLag");
    // selecter.addEventListener("change", selectChanges);

    // //
    // function addOption(e) {
    //     if(login.value === "" ) {
    //         e.preventDefault();
    //         return;
    //     }

    //     const temp = login.value;
    //     const opt = selecter.selectLanguages.options;
    //     opt[opt.length] = new Option(temp, temp);
    //     selecter.selectLanguages.size += 1;
    //     console.log(temp);
    // }

    // function removeOption(e){
    //     const sel = selecter.selectLanguages.options;
    //     sel[sel.selectedIndex] = null
    //     selecter.selectLanguages.size -= 1;
    // }

    // const buttonRemove = document.registrationForm.buttonRemove;
    // subm.addEventListener("click", addOption);
    // buttonRemove.addEventListener("click", removeOption);
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Базовая работа с BOM, ПЕРЕАДРЕСАЦИЯ, ИСТОРИЯ, ОКНА и т.д.
    //window.alert("afd;kjl;lkadfn"); // alert("afd;kjl;lkadfn");
    // var message = "message"; // var - глобальная (window)
    // console.log(window.message);

    // //alert = function(message){let a = 5; console.log(5 + message);} // переопределился alert()
    // //alert(6); // 11

    // let doc = document.documentElement;   

    // console.log(window.innerHeight, window.outerWidth);
    // console.log(doc.clientHeight, doc.clientWidth);
    // console.log(window.locationbar);
    // console.log(screen.orientation);

    // // print("Print"); // Не смотря на то, что вызывается первым по настоящему выполнится последним
    // // alert("Alert");
    // // confirm("Confirm"); 
    // // prompt("Prompt");
    // // alert(find("Новое"));
    // let openWindow = null;

    // const send = document.registrationForm.button;
    // const close = document.registrationForm.buttonRemove;
    // send.addEventListener("click", ()=>{
    //     if (!openWindow || openWindow.closed)
    //         openWindow = window.open("https://metanit.com", "_blank","location=false,menubar=false,popup=true,scrollbars=false,status=false,toolbar=false,height=480,width=640");
    // });
    // close.addEventListener("click", ()=>{
    //     openWindow?.close();
    //     openWindow = null;
    // });

    // //document.body.addEventListener("click", ()=>openWindow?.scrollBy(0, 50)); // не работает, т.к. разные источники (file, https)

    // // Не доделана. Не получилось т.к. страница запущена через файл, а не через сервер.
    // console.log(history.length, history.state);

    // const pages = {
    //     java:{content: "Java", url:"/java"},
    //     cSharp:{content: "C#", url:"/cSharp"},
    //     cPlusPlus:{content: "C++", url:"/cPlusPlus"},
    // }
    // const contentPage = document.getElementById("content");

    // function historyPages(event){
    //     const url = event.target.getAttribute("href");
    // }

    // //history.pushState(pages.java, "Java", pages.java.url);

    // window.addEventListener("popstate", (event)=>{
    //     if(event.state)
    //         contentPage.textContent = event.state.content;
    // });

    // const form = document.myForm;
    // form.addEventListener("click", (event)=>{
    //     //if(event.target.name = "languages")
            
    // });
    // // Не получилось. Чтобы работало должны совпадать протоколы

    // console.log(location.pathname);
    // console.log(location.href);
    // console.log(location.protocol);
    // console.log(location.host);
    // console.log(location.port);

    // //location.assign("https://www.metanit.com"); // Также есть location.replace() - отличается тем, что не сохраняет в истории пред. страницу
    // //location.reload();

    // // navigator имеет ряд свойств окружения
    // console.log("NAVIGATOR");
    // console.log(navigator.geolocation);
    // console.log(navigator.onLine);
    // console.log(navigator.platform);
    // console.log(navigator.vendor); // и т.д.

    // // Таймеры
    // /*const time = */ setTimeout(()=>console.log("Время прошло"), 1000); // Единоразово
    // // clearTimeout(time);
    // setInterval(()=>console.log("Время прошло"), 1000); // Постоянно
    // // аналогично clearTimeout() - clearInterval();

    // // Для анимаций чаще используется window.requestAnimation();
    // function move(){
    //     if(offset > 600) step -= 10;
    //     if(offset < 200) step += 10;
    //     offset += step;
    //     // document.getElementById(<какой-нибудь элемент>).style.marginLeft = offset + "px";
    //     window.requestAnimationFrame(move);
    // }

    // var step = 10;
    // window.requestAnimationFrame(move);
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Сериализация и десериализация JSON и XML
    // JSON
    // const user = {
    //     name: "Tom",
    //     age: 18
    // }

    // const serialized = JSON.stringify(user);
    // console.log(serialized);

    // const deserialized = JSON.parse(serialized);
    // console.log(deserialized);

    // // XML
    // const xml = 
    // `<?xml version="1.0" encoding="UTF-8" ?> 
    // <users>
    //     <user name="Tom" age="39">
    //         <company>
    //             <title>Microsoft</title>
    //         </company>
    //     </user>
    //     <user name="Bob" age="43">
    //         <company>
    //             <title>Google</title>
    //         </company>
    //     </user>
    // </users>`;

    // // Гораздо неудобнее по сравнению с JSON
    // const domParser = new DOMParser();
    // const xmlParser = domParser.parseFromString(xml, "text/xml");
    // const firstUser = xmlParser.querySelector("user");

    // console.log(firstUser.getAttribute("name")); //Tom
    // console.log(firstUser.getAttribute("age")); // 39
    // console.log(firstUser.querySelector("title").textContent); // Microsoft

    // // десериализация
    // const xmlSerializer = new XMLSerializer();
    // const deserializedXML = xmlSerializer.serializeToString(xmlParser);
    // console.log(deserializedXML); 
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Cookie, sessionStorage, localStorage
    // const d = new Date();
    // d.setSeconds(d.getSeconds()+10);
    // document.cookie="login=vasilevich;expires="+ d.toUTCString() + ";";
    // document.cookie="age=18";
    // // path - задает путь по которому используется куки (домен/path), domain - непременно домен
    // // secure - использовать ли SSL 
    // const cook = document.cookie.split("; ");
    // for(c of cook)
    // {
    //     const parts = c.split("=");
    //     console.log(c);
    //     console.log(`Имя - ${parts[0]}`);
    //     console.log(`Значение - ${parts[1]}`);
    // }
    
    // document.cookie="age=;Max-Age=-1;"; // Удаляет cookie - age. Max-Age=-1 - служит для указания незамедлительного удаления
    
    // // localStorage | sessionStorage
    // localStorage.setItem("localStorage", "localStorage");
    // sessionStorage.setItem("sessionStorage", "sessionStorage");

    // const user = {
    //     name: "Tom",
    //     age: "18",
    //     married: true
    // }

    // localStorage.setItem("localUser", JSON.stringify(user));
    // sessionStorage.setItem("sessionUser", JSON.stringify(user));

    // localStorage.removeItem("user"); // удаляет user
    // sessionStorage.removeItem("user"); // удаляет user

    // const ds = localStorage.getItem("localUser"); // {"name":"Tom","age":"18","married":true}
    // console.log(JSON.parse(ds)); // {name: 'Tom', age: '18', married: true}
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Promise, async/await
// function asyncFunction(){
//     let x = 0;
//     // setInterval(() => {
//     //     console.log(`Текст номер: ${++x}`);
//     // }, 1000);
// }

// asyncFunction();

// function useCallback(callback){
//     const math = Math.random() * 100;
//     if (math < 50) {
//         callback(new Error(`Ошибка числа на входе: ${math}`), null);
//     }
//     else{
//         callback(null, math);
//     }
// }

// function ownCallback(error, result){
//     if(error) console.error(error);
//     else console.log(`Результат: ${result}`);
// }

// useCallback(ownCallback);

// function second(){
//     console.log("Начало второго метода");
//     setTimeout(()=>console.log("meeting 2"), 2000);
// }
// function first(){
//     console.log("Начало первого метода");
//     setTimeout(()=>console.log("meeting 1"), 1000);
// }

// second();
// first();

// const promisNomer3 = new Promise((resolve)=>{
//     setTimeout(()=>console.log("3 секунды"), 3000);

//     resolve();
// });
// const promisNomer2 = new Promise((resolve)=>{
//     setTimeout(()=>console.log("2 секунды"), 2000);

//     resolve();
// });
// const promisNomer1 = new Promise((resolve, reject)=>{
//     setTimeout(()=>console.log("1 секунда"), 1000);

    
//     resolve("Возвращение значения номер один");

//     //reject("Ошибка");
// });

// function res(zn){
//     return new Promise(function(resolve){
//         console.log(zn);
//         resolve("Плевать хотел на результат");
//     });
// }

// // Главное каждый .then должен возвращать значение, чтобы последующий смог его обработать
// promisNomer1
//     .then(function(value, res){ 
//         let v = value; 
//         console.log(v);
//         throw "Ошибка"; // Перейдем в .catch
//         })
//     .then(resu => res(resu)) 
//     .catch(err => console.error(err));
// // или
// promisNomer1
//     .then(value => {
//         console.log("Первый then:", value);
//         return new Promise((resolve, reject)=>reject("Ошибка в одном из промисов"));   // перейдем в .catch
//     }/*, alert("Ошибка из модуля then")*/) // можно обрабатывать ошибку и из .then .then(resolve, reject) 
//     .then(resu => res(resu))  // передаём дальше
//     .then(final => console.log("Финал:", final))
//     .catch(err => console.error("Ошибка:", err));
// // или
// promisNomer1
//     .then(res)
//     .then(console.log)
//     .catch(console.error) // catch возвращает Promis. Может возвращать значение
//     .then(console.log("Конец")) // вызывается вне зависимости от catch
//     .finally("FINALLY окончательный конец"); // finally возвращает Promis. Может возвращать значение

// Promise.all([promis1, promis2, ...]) - все промисы выполняются одновременно, упал один - упали все
// Promise.allSelected([promis1, promis2, ...]) - выполняются одновременно, возвращают статусы каждого промиса
// Promise.race([promis1, promis2, ...]) - возвращает первый выполнившийся промис
// Promise.any([promis1, promis2, ...]) - возвращает первый успешно выполнившийся промис


}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Fetch
// fetch("/hello")
//     .then(response => response.text())
//     .then(console.log);

//     // response имеет след. свойства только для чтения
//     // body, bodyUsed, status, ok, redirected, type, url
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Export, Import и Await Import
    // //import {myAlert as myConsole} from "./export.js"; // псевдонимы можно использоать и при export и при import
    // import * as ImportModule from "./export.js";
    // await import("./export.js").then((module)=>{ // если есть await, то это динамический модуль и может загружаться при каком-либо условииы
    //     module.default();
    // })
    // //myConsole();

    // document.cookie=`znach=${ImportModule.znach}`; // znach=5
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Canvas
    // const canvas = document.getElementById("canvas");
    // canvas.getContext("2d").fillRect(50,100,200,100); // x = 50, y = 100, ширина = 200, высота = 100
//     const canvas = document.getElementById("canvas");
// const context = canvas.getContext("2d");
 
// const w = canvas.width;
// const h=canvas.height;
 
// const mouse = { x:0, y:0};      // координаты мыши
// let draw = false;
              
// // нажатие мыши
// canvas.addEventListener("mousedown", function(e){
      
//     mouse.x = e.pageX - this.offsetLeft;
//     mouse.y = e.pageY - this.offsetTop;
//     draw = true;
//     context.beginPath();
//     context.moveTo(mouse.x, mouse.y);
// });
// // перемещение мыши
// canvas.addEventListener("mousemove", function(e){
      
//     if(draw==true){
      
//         mouse.x = e.pageX - this.offsetLeft;
//         mouse.y = e.pageY - this.offsetTop;
//         context.lineTo(mouse.x, mouse.y);
//         context.stroke();
//     }
// });
 
// // отпускаем мышь
// canvas.addEventListener("mouseup", function(e){
      
//     mouse.x = e.pageX - this.offsetLeft;
//     mouse.y = e.pageY - this.offsetTop;
//     context.lineTo(mouse.x, mouse.y);
//     context.stroke();
//     context.closePath();
//     draw = false;
// });
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // IndexDB | База Данных
// const db = window.indexedDB;
// console.log(db);

// const request = indexedDB.open("firstDatabase");
// console.log(request);

// request.onerror = (event) =>{ // проверка на ошибки 
//     console.log(event.target.error);
// };   

// request.onupgradeneeded = (event)=>{ // возникает при создании
//     console.log(event.oldVersion); // 0
//     console.log(event.newVersion); // 1

//     const db = event.target.result;
//     if (!db.objectStoreNames.contains("users")) {
//         db.createObjectStore("users", {
//             keyPath: "id",
//             autoIncrement: false
//         });
//     } // создает сущность (таблица)
//     console.log("База данных users создана");
//     console.log(db.objectStoreNames); // все таблицы бд

//     //db.deleteObjectStore("users"); // удаляет сущность (таблица)
// };

// console.log(indexedDB.databases());

// request.onsuccess = (event) =>{ // видимо работает либо асинхронно либо после всех основных запросов
//     const db = event.target.result;

//     console.log(db.name); // event.target.result - получаем объект базы данных с его свойствами

//     { // обязательны для всей работы с бд
//         const transact = db.transaction(["users"], "readwrite"); // readonly - чтение | readwrite - чтение/запись | versionchange - полные права
//         var users = transact.objectStore("users");
//     }

//     users.clear();

//     const bob1 = { id:34, name:"Bob", age:19};
//     const bob2 = { id:35, name:"Bob", age:19};

//     users.put(bob1).onerror = (e) => { // Чаще используют put, т.к. он и добавляет или обнавляет 
//         console.log("Ошибка add:", e.target.error);
//     };

//     const addRequest = users.add(bob2);

//     addRequest.onsuccess = (event) => {
//         console.log("Данные успешно добавлены");
//         console.log("id добавленной записи:", addRequest.result); 
//     };

//     addRequest.onerror = (e) => {
//         console.log("Ошибка add:", e.target.error);
//     };

//     // всегда возвращает массив
//     const all = users.getAll(/*число - ключ | спец. код*/); // пример кода (ключ больше x): IDBKeyRange.lowerBound(x, true); 
//     all.onsuccess = () => {
//         console.log(all.result);
//     };

//     // возвращает объект или undefined
//     const one = users.get(34); // пример кода (ключ больше x): IDBKeyRange.lowerBound(x, true); 
//     one.onsuccess = () => {
//         console.log(one.result);
//     };

//     users.put({name:"Tom", age:19, id:34 });

//     const Bob = users.get(35);
//     Bob.onsuccess = ()=>{
//         let bob = Bob.result;

//         bob.age = 120;

//         users.put(bob);
//     }
//     // event.target.result.close(); // перед удалением закрываем подключение к бд
//     // indexedDB.deleteDatabase("firstDatabase").onsuccess = ()=>console.log("Удалено");

//     const kol = users.count(); // аналогично getAll()
//     kol.onsuccess = ()=> console.log("Количество:",kol.result);

//     const del = users.delete(34); // аналогично count() и getAll()
//     del.onsuccess = ()=>console.log("Удаление:", del.result);

//     const cursor = users.openCursor();
//     const arrayOfUsers = [];
    
//     cursor.onsuccess = ()=>{
//         if(cursor.result){
//             arrayOfUsers.push(cursor.result.value);
//             cursor.result.continue();
//         }
//         else{
//             arrayOfUsers.forEach(console.log)
//             console.error("Массив пуст")
//         }
//     };
// };

//     transaction.oncomplete = () => {
//         console.log("Транзакция завершена");
//         console.log(arrayOfUsers);
//     };
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Drag and Drop
    // let dragged = null; // при const не удаляется 

    // const box = document.getElementById("blueRect");

    // box.addEventListener("dragstart", (e)=>dragged = e.target);

    // const space = document.getElementById("dragged");
    // space.addEventListener("dragover", (e)=>e.preventDefault());
    // space.addEventListener("drop", (e)=>{
    //     //e.preventDefault();
    
    //     e.target.appendChild(dragged); // объект перемещается, т.к. у DOM может быть только 1 ссылка

    //     //e.target.appendChild(box.cloneNode(true)); // cloneNode() - копирует только внешнюю часть
    //     //dragged.parentNode.removeChild(dragged);
    // });
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Чтение файлов/FileReader
    // function handleDragover(e){
    //     e.preventDefault();
    //     // четко указывается что файл копируется
    //     e.dataTransfer.dropEffect = "copy"; // move, link, none
    // }
    // function printFiles(e){
    //     e.preventDefault();
    //     const files = e.dataTransfer ? e.dataTransfer.files : e.target.files; 
    //     let output = "";
    //     for(let i = 0; i < files.length; i++){
    //         const file = files[i];
    //         output += "<li><p><strong>" + file.name + "</strong></p>";
    //         output += "<p>Type: " + file.type + "</p>";
    //         output += "<p>Size: " + file.size + "</p>";
    //         output += "<p>Last Modification: " + new Date(file.lastModified).toLocaleDateString(); + "</p></li>";
    //     }

    //     document.getElementById("fileList").innerHTML = "<ul>" + output + "</ul>";
    // }

    // const dragged = document.getElementById("dragged");
    // dragged.addEventListener("dragover", handleDragover);
    // dragged.addEventListener("drop", printFiles);


    // const progressbar = document.getElementById("progress-bar");
    // const progress = document.getElementById("progress");

    // function readFile(e){
    //     const files = e.target.files;
    //     console.log(files);

    //     for (file of files){
            
    //          // создается в цикле для возможности чтения нескольких выбранныз файлов
    //         const reader = new FileReader();
    //         // событие для каждого нового reader
    //         reader.onload = (e) =>{ // т.к. событие асинхронное бывают ошибки неправильно захватываемых переменных
    //             console.log(e.target.result); //то же самое? что и reader.result
    //             console.log("============");
    //             progress.style.width = "100%";    
    //             progress.textContent = "100%";
    //         }
    //         reader.onprogress = progressFunc;

    //         if(file.type.match("text.*")) console.log("Текстовый файл с именем:", file.name);

    //         // Вариант более правильного решения, т.к. теперь захватывается отдельная переменная
    //         // reader.onload = (function(fileData) {
    //         //     return function(e){
    //         //         console.log("File Name:", fileData.name);
    //         //         console.log(e.target.result); // то же самое, что и reader.result
    //         //         console.log("==============================");
    //         //     };
    //         // })(file);

    //         reader.readAsText(file);
    //     }
    //     //document.getElementById("fileList").innerHTML = str;
    // } 

    // function progressFunc(e){
    //     if(e.lengthComputable > 0){
    //         const percentLoaded = Math.round((e.loaded/e.total) * 100);
    //         progress.style.width = percentLoaded + "%";      
    //         progress.textContent = percentLoaded + "%";
    //     }
    // } 

    // const fileInput = document.getElementById("fileInput");
    // fileInput.addEventListener("change", readFile);
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
{ // Worker
// // Создаём Worker (переименовали переменную)
// const worker = new Worker("export.js");

// // Получаем сообщения от Worker
// worker.onmessage = (e) => {
//     console.log("Сообщение от Worker:", e.data);
// };

// // Отправляем команду на запуск
// worker.postMessage("start");
}
////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////

    import React from "https://esm.sh/react@19?dev";
    import ReactDom from "https://esm.sh/react-dom@19/client?dev";

    const rootNode = document.getElementById("fileList");    // элемент для рендеринга приложения React
    // получаем корневой элемент 
    const root = ReactDom.createRoot(rootNode);
    // создаем заголовок - элемент h1
    //const element = React.createElement("h1", null, "Hello METANIT.COM");
    const element = <div>Hello Metanit.com {55 + 55}</div>;
    // рендеринг в корневой элемент
    root.render(element);

////////////////////////////////////////////////////////////////////////