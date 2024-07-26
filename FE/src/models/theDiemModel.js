const toJson = (item) => {
    return {
        id: item.id,
        tenThe: item.tenThe,
        loaiThe : item.loaiThe,
        diemCanTren : item.diemCanTren,
        diemCanDuoi: item.diemCanDuoi,
    }
}

const fromJson = (item) => {
    return {
        id: item.id,
        tenThe: item.tenThe,
        loaiThe : item.loaiThe,
        diemCanTren : item.diemCanTren,
        diemCanDuoi: item.diemCanDuoi,
    }
}

const baseJson = () => {
    return {
        id: null,
        tenThe: null,
        loaiThe : null,
        diemCanTren : 0,
        diemCanDuoi: 0,
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
export const theDiemModel = {
    toJson, fromJson, baseJson, toListModel
}
