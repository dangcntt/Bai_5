const toJson = (item) => {
    return {
            id: item.id,
            lydo : item.lydo,
            vanban : item.vanban,
    }
}

const fromJson = (item) => {
    return {
        id: item.id,
        lydo : item.lydo,
        vanban : item.vanban,
    }
}

const convertJson = (item) => {
    return {
        id: item.id,
        lydo : item.lydo,
        vanban : item.vanban,
    }
}

const baseJson = () => {
    return {
        id: null,
        lydo : null,
        vanban : null,
    }
}

const toListModel = (items) =>{
    if(items.length > 0){
        let data = [];
        items.map((value, index) =>{
            data.push(fromJson(value));
        })
        return data??[];
    }
    return [];
}
export const deleteModel = {
    toJson, fromJson, baseJson, toListModel , convertJson
}
