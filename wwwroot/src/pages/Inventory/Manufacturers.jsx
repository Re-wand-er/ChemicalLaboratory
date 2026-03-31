import { useState, useEffect } from "react";

const Manufacturers = () => {
    // const [data, setData] = useState(null); // []

    // useEffect(() =>{
    //     fetch('/api/manufacturer/')
    //         .then(res => res.json())
    //         .then(result =>{
    //             setData(result);
    //         });
    // }, []);    

    return (
        <div>
            <h2>Производители</h2>
            {/* {data ? data.map(item => (
                <h4 key={item.id}>{item.id} {item.reagentId} {item.notificationType} {item.message} {item.createdAt} {item.isRead}</h4>
            )) : 
                <h4>Ошибка загрузки данных</h4>
            } */}

        </div>
    );
};
export default Manufacturers;