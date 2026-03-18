import { useState, useEffect } from "react";

const Reagents = () => {
    const [data, setData] = useState(null); // []

    useEffect(() =>{
        fetch('/api/reagent/')
            .then(res => res.json())
            .then(result =>{
                setData(result);
            });
    }, []);    

    return (
        <div>
            <h2>Реагенты</h2>
            {data ? data.map(item => (
                <h4 key={item.id}>
                    {item.id} {item.name} {item.chemicalFormula} {item.unit} {item.currentQuantity} {item.minQuantity}
                    {item.expirationDate} {item.storageLocation} {item.categoryId} {item.createdAt} {item.isActive} 
                </h4>
            )) : 
                <h4>Ошибка загрузки данных</h4>
            }

        </div>
    );
};
export default Reagents;