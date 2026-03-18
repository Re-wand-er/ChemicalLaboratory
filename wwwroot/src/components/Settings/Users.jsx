import { useState, useEffect } from "react";

const Users = () => {
    const [data, setData] = useState(null); // []

    useEffect(() =>{
        fetch('/api/user/')
            .then(res => res.json())
            .then(result =>{
                setData(result);
            });
    }, []);    

    return (
        <div>
            <h2>Пользователи</h2>
            {data ? data.map(item => (
                <h4 key={item.id}>
                    {item.id} {item.idWorkSchedule} {item.firstName} {item.middleName} {item.lastName} {item.email}
                    {item.sex} {item.systemRole} {item.jobPosition} {item.login} {item.isActive} 
                </h4>
            )) : 
                <h4>Ошибка загрузки данных</h4>
            }

        </div>
    );
};
export default Users;