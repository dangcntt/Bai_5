import moment from "moment";
import {thongTinHangHoaModel} from "@/models/thongTinHangHoaModel";

const toJson = (item) => {
    return {
        id: item.id,
        ngayLapHoaDon : item.createdAtShow,
        hinhThucThanhToan : item.hinhThucThanhToan,
        khachHang : item.khachHang,
        tenKhachHang : item.tenKhachHang,
        tenDonVi : item.tenDonVi,
        maSoThue : item.maSoThue,
        taiKhoanNganHang : item.taiKhoanNganHang,
        diaChi : item.diaChi,
        tongTien : item.tongTien,
        list : SendJsonForList(item.list)
    }
}


const SendJsonForList = (list) => {

    let arr = [];
    list.forEach((value, index) => {
        // console.log("LOG SEND JSON FOR LIST ", ele)
        arr.push(thongTinHangHoaModel.sendJson(value));
    });
    return arr;
}

const baseJson = () => {
    return {
        id: null,
        createdAtShow : null,
        ngayLapHoaDon : moment().format("DD/MM/yyyy"),
        hinhThucThanhToan : null,
        khachHang : null,
        tenKhachHang : null,
        tenDonVi : null,
        maSoThue : null,
        taiKhoanNganHang : null,
        diaChi : null,
        tongTien : 0,
        list : []
    }
}

export const hoaDonModel = {
    baseJson , toJson
}
