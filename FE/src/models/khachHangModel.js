const toJson = (item) => {
    return {
        id: item.id,
        tenKhachHang: item.tenKhachHang,
        tenDonVi: item.tenDonVi,
        maSoThue:item.maSoThue,
        taiKhoanNganHang:item.taiKhoanNganHang,
        diaChi:item.diaChi
    }
}

const fromJson = (item) => {
    return {
        id: item.id,
        tenKhachHang: item.tenKhachHang,
        tenDonVi: item.tenDonVi,
        maSoThue:item.maSoThue,
        taiKhoanNganHang:item.taiKhoanNganHang,
        diaChi:item.diaChi
    }
}

const convertJson = (item, collectionName) => {
    return {
        id: item.id,
        code: item.code,
        name: item.name,
        collectionName: collectionName
    }
}

const baseJson = () => {
    return {
        id: null,
        tenKhachHang: null,
        tenDonVi: null,
        maSoThue:null,
        taiKhoanNganHang:null,
        diaChi:null
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
export const khachHangModel = {
    toJson, fromJson, baseJson, toListModel , convertJson
}
