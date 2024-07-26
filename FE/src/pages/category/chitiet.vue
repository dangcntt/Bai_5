<script>
import Layout from "@/pages/congdan/layout";
import Multiselect from "vue-multiselect";

/**
 * Crypto ICO-landing page
 */
export default {
  components: { Layout},
  data() {
    return {
      data: [],
      start: 2,
      // url: `${process.env.VUE_APP_API_URL}files/view`,
      totalRows: 1,
      numberOfElement: 1,
      perPage: 10,
      currentPage: 1,
      sortBy: "age",
      sortDesc: false,
      model:[],
      filterOn: [],
      pageOptions: [5,10,25,50,100],
      url: `${process.env.VUE_APP_API_URL}filesminio/view`,
      apiView: `${process.env.VUE_APP_API_URL}filesminio/view/`,
      tinlienquan:[],
      listDanhMuc :[],
    };
  },
  computed: {

  },
  watch: {
    $route(to, from) {
      this.getGioiThieu();
      this.getTinLienQuan();
    },
    'model._id': function(newVal, oldVal) {
      // Handle the change in _id here
      this.getTinLienQuan();
      console.log(`_id changed from ${oldVal} to ${newVal}`);
    },
  },
  created() {
    console.log("LOG CREATED LIEN HE ")
    window.addEventListener("scroll", this.windowScroll);
    this.GetDanhSach();
    this.getGioiThieu();
    this.getTinLienQuan();
  },
  destroyed() {
    window.removeEventListener("scroll", this.windowScroll);
  },
  mounted() {
    this.start = new Date(this.starttime).getTime();
    this.end = new Date(this.endtime).getTime();
    this.menuId = this.$route.query.menuId;
    // Update the count down every 1 second
  },
  methods: {
    async GetDanhSach(){
      await this.$store.dispatch("menuCongDanStore/getDanhMuc").then((res) => {
        this.listDanhMuc = res.data;
        //    console.log("LOG DANH MUC : ", this.listDanhMuc)
      })
    },
    async getGioiThieu(){
      await this.$store.dispatch("contentStore/getById",{
        "_id": this.$route.params.id
      } ).then((res) => {
        this.model = res.data;
        this.model.noiDung = this.model.noiDung.replaceAll("https://localhost:5001/api/v1/filesminio/view" , this.url);
        // console.log("MODEL BAI VIET : ", this.model)
      })
    },
    handleShowRegisterModal(){
      this.$store.dispatch("snackBarStore/showRegisterModal", !this.$store.state.snackBarStore.registerModal)
      console.log("abc")
    },
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    /**
     * Window scroll method
     */
    windowScroll() {
      const navbar = document.getElementById("navbar");
      if (
          document.body.scrollTop >= 50 ||
          document.documentElement.scrollTop >= 50
      ) {
        navbar.classList.add("nav-sticky");
      } else {
        navbar.classList.remove("nav-sticky");
      }
    },
    /**
     * Toggle menu
     */
    toggleMenu() {
      document.getElementById("topnav-menu-content").classList.toggle("show");
    },
    nextSlide() {
      this.$refs.carousel.goToPage(this.$refs.carousel.getNextPage());
    },
    prevSlide() {
      this.$refs.carousel.goToPage(this.$refs.carousel.getPreviousPage());
    },
    normalizer(node) {
      if (node.children == null || node.children == 'null') {
        delete node.children;
      }
    },
    getColorWithExtFile(ext) {
      if (ext == '.docx' || ext == '.doc')
        return 'text-primary';
      else if (ext == '.xlsx' || ext == '.xls')
        return 'text-success';
      else if (ext == '.pdf')
        return 'text-danger';
      else return 'text-danger';
    },

    getIconWithExtFile(ext) {
      if (ext == '.docx' || ext == '.doc')
        return 'mdi mdi-microsoft-word';
      else if (ext == '.xlsx' || ext == '.xls')
        return 'mdi mdi-microsoft-excel';
      else if (ext == '.pdf')
        return 'mdi mdi-file-pdf-outline';
      else if (ext == '.pptx')
        return 'fas fa-file-powerpoint';
      else return 'mdi-file-clock-outline';
    },
    getUrl(item) {
      // console.log("LOG DANH MUC CLICK", item)
      if (item.link.indexOf("/{id}") < 0  && item.level === 0)
      {
        // console.log("LOG ROUTER IF LAYOUT  : ", item)
        this.idMenu = item.id;
        //  console.log("LOG ITEM : ", item.link)
        this.$router.push(item.link);
      } else if (item.link.indexOf("/{id}") > 0 && item.level === 0){
        this.idMenu = item.id;
        // console.log("LOG ROUTER IF ELSE  LAYOUT  : ", item.link.replace("{id}",  item.id))
        this.$router.push(item.link.replace("{id}",  item.id));
      }else {
        // console.log("LOG ROUTER ELSE LAYOUT  : ", item.link +   item.id)
        this.idMenu = item.id;
        this.$router.push(item.link +  "/" + item.id);
      }
    },
    async getTinLienQuan(){
      const params = {
        start: 0,
        limit: 4,
        menuId : this.menuId,
        contentId: this.model._id
      };
      if( this.model._id != null){
        await this.$store.dispatch("contentStore/getTinKhac",params).then((res) => {
          if (res.code===0) {
            this.tinlienquan = res.data.data;
            console.log('log tin lien quan', this.tinlienquan)
          }
        })
      }
    },

  },
};
</script>

<template>
  <Layout>
    <section class="section p-2 bg-white" id="about">
        <div class="row align-items-center">
          <div>
            <div class="row">
              <div class="col-12">
                <div class="row">
                  <div class="col-lg-12">
                    <div class="container">
                      <div class="row">
                        <div class="col-lg-9 mb-3">
                          <div class="title mt-4 mb-4">
                            <h2>
                                {{ this.model.name }}
                            </h2>
                          </div>
                          <div v-html="model.noiDung" class="noidung" style="font-size: 14px;">
                          </div>
                          <div v-for ="(item,index) in this.model.files" :key="index" style="margin-top: 20px; border-top: 1px solid #ccc;">
                              <a
                                  :href="`${url}/${item.fileId}`"
                                  class="text-dark fw-medium d-flex justify-content-between align-items-center">
                                  <div class="font-size-13">
                                     <span  style="font-weight: bold; color: #990000;">TỆP ĐÍNH KÈM
                                      <i class="mdi mdi-chevron-double-right font-size-20" aria-hidden="true"></i>
                                    </span>
                                  <i
                                      :class="`${getColorWithExtFile(item.ext)} me-2 ${getIconWithExtFile(item.ext)}`"
                                  ></i>
                                  <span style="font-weight: bold;color: rgba(var(--bs-link-color-rgb), var(--bs-link-opacity, 1));">{{ item.fileName }}</span>
                                  </div>
                              </a>
                          </div>
                        </div>


                        <div class="col-lg-3">
                          <div class="category">
                            <div class="cate-title">
                              Danh Mục
                            </div>
                            <div v-for ="(item,index) in listDanhMuc" :key="index"  class="cate-list">
                              <ul>
                                <li style="cursor: pointer">
                                  <i class="mdi mdi-chevron-double-right"></i>
                                  <a @click="getUrl(item)">{{item.label}}</a>
                                </li>
                              </ul>
                            </div>
                          </div>
                        </div>
                      </div>
                      <template v-if="tinlienquan.length>0">
                        <div class="related row">
                          <div class="col-12 mb-2">
                            <div class="cs-title-box">
                              <div class="cs-title py-2">
                                <a href="">
                                  <i class="mdi mdi-star ic-item"></i>
                                  <span style="color: #fff; font-size: 16px;">Tin liên quan</span>
                                </a>
                              </div>
                            </div>
                          </div>

                          <section>
                            <div class="row">
                                <div
                                  v-for="(item,index) in tinlienquan" :key="index"
                                    class="col-md-12 col-lg-6"
                                >
                                  <div class="col-md-12 mb-4">
                                    <b-card no-body class="card-box">
                                      <b-row no-gutters class="align-items-center">
                                        <b-col md="5">
                                          <router-link
                                                :to="{
                                                    path: `/bang-tin/chi-tiet/${item._id}`,
                                                    }"
                                            >
                                            <div v-if="!item.fileImage">
                                              <div class="float-left">
                                                <img
                                                    src="@/assets/images/logoXSKTDT.png"
                                                    alt="Không có ảnh"
                                                    class="rounded-0 w-100 img-tin"
                                                    style="height: 250px; padding: 20px;"
                                                >
                                              </div>
                                            </div>
                                            <div v-else>
                                                <b-card-img
                                                    :src="apiView + item.fileImage.fileId"
                                                    class="rounded-0 img-tin"
                                                    :height="250"
                                                    :width="250"
                                                ></b-card-img>
                                            </div>
                                          </router-link>
                                        </b-col>
                                        <b-col md="7" style="padding: 10px;" class="title-category">
                                          <router-link
                                                :to="{
                                                    path: `/bang-tin/chi-tiet/${item._id}`,
                                                    }"
                                            >
                                            <b-card-body class="" :title="`${item.name}`" style="color: #000 !important;">
                                              <p class="card-text">
                                              </p>
                                              <b-card-text
                                                  class="fs-13 custom-content"
                                              >
                                                  <p v-html="item.moTa" style="padding-right: 10px;  color: #495057 !important;"></p>
                                              </b-card-text>

                                                <b-button
                                                    pill
                                                    class="px-4 fs-13"
                                                    size="sm"
                                                    style="
                                                      float: left;
                                                      background-color:#efc62c;
                                                      color: #000 !important;
                                                      border: none;
                                                      "
                                                >
                                                  Chi tiết
                                                  <i class="mdi mdi-arrow-right ps-2"></i>
                                                </b-button>
                                            </b-card-body>
                                          </router-link>
                                        </b-col>
                                      </b-row>
                                    </b-card>
                                  </div>
                                </div>
                            </div>

                          </section>
                        </div>
                      </template>
                      <div class="row d-flex align-items-baseline mb-3">
                        <div class="col-12 position-relative pt-4">
                          <a
                              class="back"
                              @click="$router.go(-1)"
                          >
                            <i class="bx bx-left-arrow-alt me-2 text-white"></i>
                            Quay lại
                          </a>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
    </section>
  </Layout>
</template>
<style>

.category{
  background-color: #f9f9f9;
}

.cate-title{
  background-color: #05912a;
  color: #fff;
  padding: 10px;
  font-size: 20px;
}

.cate-list ul li{
  list-style: none;
  border-bottom: 1px dashed #d0cfcf;
  padding: 10px 0 10px 0;
}

.cate-list ul li a{
  margin-left: 10px;
  font-size: 14px;
  color: #78797C;
}

/* .cs-title-box .cs-title .ic-item {
  background-color: #fff;
  color: #d60604;
  padding: 5px 7px;
  border-radius: 50px;
  margin-right: 10px;
}

.cs-title-box .cs-title {
  background-color: #d60604;
  color: #fff;
  width: fit-content;
  padding: 5px;
  padding-right: 20px;
  border-radius: 50px;
  position: relative;
  z-index: 99;
  margin: 10px 0px;

}

.cs-title-box:before {
  display: block;
  height: 1px;
  width: 100%;
  background: linear-gradient(90deg, #d60604, rgba(199, 26, 22, 0));
  position: absolute;
  top: 50%;
  z-index: 1;
} */
.btn-yellow{
  background-color: #EFC62C;
  border: none;
  border-radius: 0 !important;
  color: #000 !important;
}

.btn-yellow:hover{
  background-color: #ffc800;
  border: none;
}

.color-primary {
  /*color: #28883b;*/
  color: #2b569a;
}

.bg-primary {
  /*background-color: #28883b !important;*/
  background-color: #2b569a !important;
}

.w-10 {
  width: 10%;
}
.w-80 {
  width: 80%;
}
.w-90 {
  width: 90%;
}

.block-ellipsis {
  display: block;
  display: -webkit-box;
  max-width: 100%;
  font-size: 14px;
  line-height: 1.4;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
  overflow: hidden;
  text-overflow: ellipsis;
}

tr {
  vertical-align: middle !important;
  box-shadow: 0 0 1rem rgb(18 38 63 / 3%) !important;
}

.bg-ico-primary {
  height: 340px;
}
.ribbons {
  /*background: linear-gradient(45deg, #940012, #F6C6C6);*/
  position: absolute;
  padding: 10px;
  font-weight: bold;
  color: #fff;
  border-radius: 5px;
  top: -18px;
  left: 20px;
  background-color: #2b569a;
  box-shadow: rgba(255, 255, 255, 0) 0px 4px 6px -1px, rgba(255, 255, 255, 0.5) 0px 2px 4px -1px;
}

@media only screen and (max-width: 425px){
  .create-at{
    text-align: start !important;
    margin-bottom: 5px;
  }
}


section.bg-ico-primary {
  padding-top: 100px;
}
.btn-detail {
  background:#2b569a;
  border: none;
}

.btn-secondary {
  --bs-btn-bg: #2b569a;
  --bs-btn-hover-bg: #537961;
}

.custom-content{
  display: -webkit-box !important;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}
.image-tracuu-pakn {
  border-bottom-left-radius: 10px !important;
  border-top-left-radius: 10px !important;
  width: -webkit-fill-available;
}

.title h2{
    border-bottom: 3px solid #ffc800;
    padding-bottom: 20px;
    text-transform: uppercase;
    color: #000;
}

.detail{
    font-size: 14px;
}

.detail .image img{
    width: 80%;
}

.detail .image{
    text-align: center;
}
.noidung figure {
    overflow: unset;
    aspect-ratio: unset;
    margin: 0 0 1rem;
}


</style>
