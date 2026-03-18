import { useState, useEffect } from "react";

const Suppliers = () => {
    const [data, setData] = useState(null); // []

    useEffect(() =>{
        fetch('/api/supplier/')
            .then(res => res.json())
            .then(result =>{
                setData(result);
            });
    }, []);    

    return (
        <div>
            <h2>Поставщики</h2>
            {data ? data.map(item => (
                <h4 key={item.id}>{item.id} {item.name} {item.contactInfo} {item.address}</h4>
            )) : 
                <h4>Ошибка загрузки данных</h4>
            }

        </div>
    );
};
export default Suppliers;