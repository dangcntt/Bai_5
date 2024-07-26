const toJson = (item) => {
    return {
        id: item.id,
        name: item.name,
        hoLot : item.hoLot,
        taiKhoan : item.taiKhoan,
        diaChiKhachHang: item.diaChiKhachHang,
        ngayThamGia: item.ngayThamGia,
        diemSo: item.diemSo,
    }
}

const fromJson = (item) => {
    return {
        id: item.id,
        name: item.name,
        hoLot : item.hoLot,
        taiKhoan : item.taiKhoan,
        diaChiKhachHang: item.diaChiKhachHang,
        ngayThamGia: item.ngayThamGia,
        diemSo: item.diemSo,
    }
}

const baseJson = () => {
    return {
        id: null,
        name: null,
        hoLot : null,
        taiKhoan : null,
        diaChiKhachHang: null,
        ngayThamGia: null,
        diemSo: 0,
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
export const baiThiModel = {
    toJson, fromJson, baseJson, toListModel
}
