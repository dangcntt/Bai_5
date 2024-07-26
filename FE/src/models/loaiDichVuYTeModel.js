import {thongTinHangHoaModel} from "@/models/thongTinHangHoaModel";
import {thuocModel} from "@/models/thuocModel";

const toJson = (item) => {
    let arr = [];
    if(item.thuoc != null)
    {
        item.thuoc.forEach((value, index) => {
            // console.log("LOG SEND JSON FOR LIST ", ele)
            arr.push(thuocModel.fromJson(value));
        });
    }
    return {
        id: item.id,
        code: item.code,
        name: item.name,
        thuoc: arr
    }
}

const fromJson = (item) => {
    return {
        id: item.id,
        code: item.code,
        name: item.name,
        thuoc: item.thuoc
    }
}



const baseJson = () => {
    return {
        id: null,
        code: null,
        name: null,
        thuoc : null
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
export const loaiDichVuYTeModel = {
    toJson, fromJson, baseJson, toListModel
}
