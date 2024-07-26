const toJson = (item) => {
    return {
        id: item.id,
        ten: item.ten,
        path: item.path,
        icon: item.icon,
        parentId: item.parentId,
        level: item.level,
        sort: item.sort,
        action: item.action
    }
}

const fromJson = (item) => {
    return {
        id: item.id,
        ten: item.ten,
        path: item.path,
        icon: item.icon,
        parentId: item.parentId,
        level: item.level,
        sort: item.sort,
        action: item.action,
        label: item.label,
        subItems: item.subItems || []
    }
}

const baseJson = () => {
    return {
        id: null,
        ten: null,
        path: null,
        icon: null,
        parentId: null,
        action: null,
        level: 0,
        sort: 0
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

export const menuModel = {
    toJson, fromJson, baseJson, toListModel
}