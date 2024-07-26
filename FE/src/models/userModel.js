const toJson = (item) => {
    return {
        id: item.id,
        userName: item.userName,
        fullName: item.fullName,
        phoneNumber: item.phoneNumber,
        email: item.email,
        note: item.note,
        avatar: item.avatar,
        donVi: item.donVi,
        roles: item.roles,
        permissions: item.permissions,
        menu: item.menu
    }
}

const fromJson = (item) => {
    return {
        id: item.id,
        usrName: item.userName,
        firstName: item.firstName,
        lastName: item.lastName,
        fullName: item.fullName,
        phoneNumber: item.phoneNumber,
        email: item.email,
        note: item.note,
        avatar: item.avatar,
        donVi: item.donVi,
        roles: item.roles,
        permissions: item.permissions,
        menu: item.menu
    }
}

const baseJson = () => {
    return {
        id: null,
        usrName: null,
        fullName: null,
        phoneNumber: null,
        email: null,
        note: null,
        avatar: null,
        roles: null
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
export const userModel = {
    toJson, fromJson, baseJson, toListModel
}
