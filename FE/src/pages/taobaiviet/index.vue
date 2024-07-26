<script>
import Layout from "@/layouts/main";
import PageHeader from "@/components/page-header";
import {numeric, required} from "vuelidate/lib/validators";
import appConfig from "@/app.config";
import Multiselect from "vue-multiselect";
import DatePicker from "vue2-datepicker";
import {VMoney} from "v-money";
import {pagingModel} from "@/models/pagingModel";
import Treeselect from "@riophae/vue-treeselect";
import {menuModel} from "@/models/menuModel";
import vue2Dropzone from "vue2-dropzone";
import {notifyModel} from "@/models/notifyModel";
import {newsModel} from "@/models/newsModel";
import axios from "axios";
import 'vue2-dropzone/dist/vue2Dropzone.min.css'

export default {
  page: {
    title: "Tạo bài viết",
    meta: [{name: "description", content: appConfig.description}],
  },
  // eslint-disable-next-line vue/no-unused-components
  components: {Multiselect, Layout, PageHeader, DatePicker,Treeselect, vueDropzone: vue2Dropzone},
  directives: {money: VMoney},
  data() {
    return {
      data: [],
      title: "Tạo bài viết",
      items: [
        {
          text: "TẠO BÀI VIẾT",
          active: true,
        }
      ],
      currentPage: 1,
      pageOptions: [5, 10, 25, 50, 100],
      sortBy: "age",
      filter: null,
      sortDesc: false,
      isShow : false,
      filterOn: [],
      numberOfElement: 1,
      totalRows: 1,
      perPage : 10,
      stt: 1,
      submitted: false,
      model: menuModel.baseJson(),
      modelContent: newsModel.baseJson(),
      pagination: pagingModel.baseJson(),
      treeView: [],
      listParent: [],
      //editor: ClassicEditor,
      editorConfig: {
        toolbar: {
          items: [
            'heading', '|','style',
            'bold',
            'italic',
            'link',
            'bulletedList',
            'numberedList',
            '|',
            'outdent',
            'indent',
            '|',
            'imageUpload',
            'blockQuote',
            'insertTable',
            'mediaEmbed',
            'codeBlock',
            'alignment',
            'ckbox',
            'fontColor',
            'fontBackgroundColor',
            'fontFamily',
            'fontSize',
            'formatPainter',
            'highlight',
            'htmlEmbed',
            'tableOfContents',
            'undo',
            'redo'
          ],
          shouldNotGroupWhenFull: false
        },
        removePlugins: ['Title', 'ImageCaption'],
        simpleUpload: {
          uploadUrl: process.env.VUE_APP_API_URL + "filesminio/uploadCK",
          headers: {
            'Authorization': 'optional_token'
          },
        },
        image: {
          toolbar: ['imageTextAlternative', '|', 'imageStyle:alignLeft', 'imageStyle:full', 'imageStyle:alignRight'],
        },

      },
      dropzoneOptions: {
        url: `${process.env.VUE_APP_API_URL}filesminio/upload`,
        acceptedFiles: ".jpg,.jpeg,.png,.gif,.pdf,.doc,.docx,.xls,.xlsx, .zip",
        thumbnailHeight: 100,
        thumbnailWidth: 300,
        maxFiles: 30,
        maxFilesize: 50,
        maxFileSizeInMB:50,
        headers: {"My-Awesome-Header": "header value"},
        addRemoveLinks: true
      },
      image: "",
      file: "",
      listMenu : [],
      isTieuDe :  false,
      isMoTa:  false,
      isTrichYeu:  false,
      isKyHieu:  false,
      isNgayXuatBan: false,
      isAnhDaiDien:  false,
      isNgayKy: false,
      isNoiDung:  false,
      isTepTin:  false,
      isShowTieuDe :  false,
      isShowMoTa:  false,
      isShowTrichYeu:  false,
      isShowKyHieu:  false,
      isShowNgayXuatBan: false,
      isShowAnhDaiDien:  false,
      isShowNgayKy: false,
      isShowNoiDung:  false,
      isShowTepTin:  false,
    };
  },
  // validations: {
  //   modelContent: {
  //     name: {required},
  //     noiDung: {required},
  //     menu:{required},
  //   },
  // },
  validations() {
    return {
      modelContent : this.rules,
    }
  },
  computed: {
   // Validations
    rules() {
        return {
          name: this.isTieuDe ? {required} : this.isTieuDe,
          noiDung: this.isNoiDung ? {required} : this.isNoiDung,
          moTa: this.isMoTa ? {required} : this.isMoTa,
          trichYeu: this.isTrichYeu ? {required} : this.isTrichYeu,
          ngayXuatBan: this.isNgayXuatBan ? {required} : this.isNgayXuatBan,
          ngayKy: this.isNgayKy ? {required} : this.isNgayKy,
          soKiHieu: this.isKyHieu ? {required} : this.isKyHieu,
          menu:{required},
          fileImage: this.isAnhDaiDien?{required}:this.isAnhDaiDien

        }
    }
  },
  created() {
    const user = {
      id: 1,
      name: 'Victory Osayi',
      is_editor: true,
      is_admin: false,
      // you can have role based permission list or access control list possibly from database
      permissions: ['manage-TAOBAIVIET-6545a309727b2a0815e25eb2', 'create-TAOBAIVIET-6545a309727b2a0815e25eb2']
    }
    this.GetTreeList();
  },
  methods: {
    onAccept(file) {
      this.image = file.name;
      this.file = file;
    },
    RemoveTree(node, instanceId) {
      let value = this.modelContent.menu?.find(x => x.id == node.id);
      if (value != null) {
        this.modelContent.menu= this.modelContent.menu.filter(x => x.id != value.id);
        // console.log("DELETE",  this.listMenu )
      }
    },
    AddTree(node, instanceId) {
      let index = this.modelContent.menu?.findIndex(x => x.id == node.id);
      if (index == -1 || index == undefined) {
        if (!this.modelContent.menu){
          this.modelContent.menu= [];
        }
        this.modelContent.menu.push(node);
        console.log('log node', node)
        this.isShowTieuDe= !this.isShowTieuDe ? node.isShowTieuDe : this.isShowTieuDe;
        this.isShowMoTa=!this.isShowMoTa ? node.isShowMoTa : this.isShowMoTa,
        this.isShowTrichYeu=!this.isShowTrichYeu ? node.isShowTrichYeu : this.isShowTrichYeu
        this.isShowKyHieu=!this.isShowKyHieu ? node.isShowKyHieu : this.isShowKyHieu
        this.isShowNgayXuatBan=!this.isShowNgayXuatBan ? node.isShowNgayXuatBan : this.isShowNgayXuatBan
        this.isShowAnhDaiDien=!this.isShowAnhDaiDien ? node.isShowAnhDaiDien : this.isShowAnhDaiDien
        this.isShowNgayKy=!this.isShowNgayKy ? node.isShowNgayKy : this.isShowNgayKy
        this.isShowNoiDung=!this.isShowNoiDung ? node.isShowNoiDung : this.isShowNoiDung
        this.isShowTepTin=!this.isShowTepTin ? node.isShowTepTin : this.isShowTepTin
        this.isTieuDe= !this.isTieuDe ? node.isTieuDe : this.isTieuDe;
        this.isMoTa=!this.isMoTa ? node.isMoTa : this.isMoTa,
            this.isTrichYeu=!this.isTrichYeu ? node.isTrichYeu : this.isTrichYeu
        this.isKyHieu=!this.isKyHieu ? node.isKyHieu : this.isKyHieu
        this.isNgayXuatBan=!this.isNgayXuatBan ? node.isNgayXuatBan : this.isNgayXuatBan
        this.isAnhDaiDien=!this.isAnhDaiDien ? node.isAnhDaiDien : this.isAnhDaiDien
        this.isNgayKy=!this.isNgayKy ? node.isNgayKy : this.isNgayKy
        this.isNoiDung=!this.isNoiDung ? node.isNoiDung : this.isNoiDung
        this.isTepTin=!this.isTepTin ? node.isTepTin : this.isTepTin
      }
    },
    // addCoQuanToModel(node, instanceId ){
    //   console.log("MENU: ", node)
    //   if(node.id){
    //     this.modelContent.menu.push({_id : node.id , name : node.label});
    //   }
    // },
    // removeCoQuanFromModel(removedNode) {
    //   // Bỏ chọn một mục, cập nhật modelContent.menu
    //   console.log("LOG BEFORE REMOVE CO QUAN : ", this.modelContent.menu)
    //   this.modelContent.menu = this.modelContent.menu.filter(item => item._id !== removedNode.id);
    //   console.log("LOG AFETER REMOVE CO QUAN : ", this.modelContent.menu)
    // },
    normalizer(node){
      if(node.children == null || node.children == 'null'){
        delete node.children;
      }
    },
    async GetTreeList(){

      await this.$store.dispatch("menuCitizenStore/getTreeList").then((res) => {
        this.treeView = res.data;
     //   console.log("LOG TREE VIEW : " , this.treeView)
      })
    },

    async handleSubmit(e) {
      console.log("LOG THONG TIN ", this.model)
      e.preventDefault();
      this.submitted = true;
      this.$v.$touch();
      console.log("LOG INVALID 36165 : ", this.$v);
      if (this.$v.$invalid) {
        console.log("LOG INVALID  : " , this.$v.$invalid)
        return;
      } else {

        let loader = this.$loading.show({
          container: this.$refs.formContainer,
        });
        await this.$store.dispatch("newsStore/create", newsModel.toJson(this.modelContent)).then((res) => {
          if (res.code === 0) {
            this.GetTreeList();
            this.showModal = false;
            this.modelContent = newsModel.baseJson();
            if (this.$refs != null && this.$refs.myVueDropzone != null)
            {
              this.$refs.myVueDropzone.removeAllFiles();
            }
            this.modelContent.noiDung = '';
          }
          this.$store.dispatch("snackBarStore/addNotify", notifyModel.addMessage(res))
          this.modelContent.ngayKy = null
        });
        loader.hide();
        this.submitted = false;
      }
    },
    uploadFile(files,response) {
      let fileSuccess = response.data;
      console.log('log suscess', response.data)
      if (response.code == 0){
        this.modelContent.files.push({
          fileId: fileSuccess.fileId,
          fileName: fileSuccess.fileName,
          ext: fileSuccess.ext,
          path: fileSuccess.path
        });
      }
      console.log('log model file', this.modelContent.files)

    },

    clearFile() {
      console.log("LOG NGUYEN TUAN KIET CLEAR ")
    },
    removeThisFile(files, error, xhr) {
      let fileCongViec = JSON.parse(files.xhr.response);
      if (fileCongViec.data && fileCongViec.data.fileId) {
        let idFile = fileCongViec.data.fileId;
        let resultData = this.modelContent.files.filter(x => {
          return x.fileId != idFile;
        })
        this.modelContent.files = resultData;
      //  console.log('log model file remove', this.modelContent.files)
      }
    },
    sendingEvent(files, xhr, formData) {
      console.log('log file', files)
      formData.append("files", files);
    },
    formatDate(dateString) {
      // Chuyển đổi định dạng ngày từ DD/MM/YYYY sang YYYY-MM-dd
      const [day, month, year] = dateString.split('/');
      const formattedDate = `${year}-${month}-${day}`;
      return formattedDate;
    },
    async upload() {
      if (this.modelContent != null && this.modelContent.fileImage != null)
      {
        console.log("LOG this.modelContent : ", this.modelContent)
        axios.post(`${process.env.VUE_APP_API_URL}filesminio/delete/${this.modelContent.fileImage.fileId}`).then((response) => {
              this.modelContent.files = null;
              console.log('log model file remove', this.modelContent.files);
            }).catch((error) => {
              // Handle error here
              console.error('Error deleting file:', error);
            });
      }
      if ( event.target &&  event.target.files.length > 0 ) {
        const formData = new FormData()
        formData.append('files', event.target.files[0])
          axios.post(`${process.env.VUE_APP_API_URL}filesminio/UploadFileImage`,formData).then((response) => {
            // console.log("LOG UPDATE : ", response);
            let resultData = response.data
            if (response.data.code == 0){
              this.modelContent.fileImage={
                fileId: resultData.data.fileId,
                fileName: resultData.data.fileName,
                ext: resultData.data.ext,
                path: resultData.data.path
              };
               console.log("LOG UPDATE : ", this.modelContent);
            }
          }).catch((error) => {
            // Handle error here
            console.error('Error deleting file:', error);
          });

      }
    },
  },
  watch: {
    model: {
      deep: true,
      handler(val){
      }
    },

    'modelContent.menu': {
      deep: true,
      handler(val){
        if (this.modelContent.menu.length==0){
              this.isTieuDe= false,
              this.isMoTa= false,
              this.isTrichYeu= false,
              this.isKyHieu=  false,
              this.isNgayXuatBan= false,
              this.isAnhDaiDien= false,
              this.isNgayKy= false,
              this.isNoiDung=  false,
              this.isTepTin= false,
              //show
              this.isShowMoTa= false,
              this.isShowTrichYeu= false,
              this.isShowKyHieu=  false,
              this.isShowNgayXuatBan= false,
              this.isShowAnhDaiDien= false,
              this.isShowNgayKy= false,
              this.isShowNoiDung=  false,
              this.isShowTepTin= false,
                  this.isShowTieuDe=false
        }
      }
    },


    // showDeleteModal(val) {
    //   if (val == false) {
    //     this.model.id = null;
    //   }
    // },
    // showPhatHanhModal (val)
    // {
    //     this.isSend = true;
    // },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items"/>
    <div class="card">
      <div class="card-body">
        <form @submit.prevent="handleSubmit"
              ref="formContainer">
          <div class="row" ref="formContainer">
            <div class="content-lis col-md-6">
              <div class="cs-title-box">
                <div class="cs-title py-2" style="padding: 10px;">
                  <span class="fw-normal font-size-13">THÔNG TIN BÀI VIẾT</span>
                </div>
              </div>
            </div>

            <div class="text-end mt-2 col-md-6">
              <b-button
                v-if="$can('manage-TAOBAIVIET-6545a309727b2a0815e25eb2') || $can('create-TAOBAIVIET-6545a309727b2a0815e25eb2')"
                type="submit"
                variant="success"
                class="ms-1 px-3 btn-luu"
              >
                Đăng bài viết
              </b-button>
            </div>
            <div class="col-12">
              <div class="mb-2">
                <label class="text-left mb-0">Chuyên mục tin</label>
                <span style="color: red">&nbsp;*</span>
                <treeselect
                    v-model="modelContent.menu"
                    v-on:select="AddTree"
                    v-on:deselect="RemoveTree"
                    :normalizer="normalizer"
                    :options="treeView"
                    :searchable="true"
                    :multiple="true"
                    :flat="true"
                    value-format="object"
                    placeholder="Chọn chuyên mục tin"
                    :class="{'is-invalid':submitted && $v.modelContent.menu.$error,}"
                >
                  <label slot="option-label" slot-scope="{ node, shouldShowCount, count, labelClassName, countClassName }" :class="labelClassName">
                    {{ node.label }}
                    <span v-if="shouldShowCount" :class="countClassName">({{ count }})</span>
                  </label>
                </treeselect>
                <div
                    v-if="submitted && !$v.modelContent.menu.required"
                    class="invalid-feedback"
                >
                  Chuyên mục tin không được trống.
                </div>
              </div>
            </div>
            <div class="col-12" v-if="isShowTieuDe==true">
              <div class="mb-2">
                <label class="text-left mb-0">Tiêu đề bài viết</label>
                <span style="color: red" v-if="isTieuDe==true">&nbsp;*</span>
                <input
                    v-model="modelContent.name"
                    id="percent"
                    type="text"
                    class="form-control"
                    placeholder="Nhập tiêu đề bài viết"
                    :class="{
                      'is-invalid':
                        submitted && $v.modelContent.name != null && $v.modelContent.name.$error,
                    }"
                />
                <div
                    v-if="submitted && $v.modelContent.name != null   && !$v.modelContent.name.required"
                    class="invalid-feedback"
                >
                  Tiêu đề không được trống.
                </div>
              </div>
            </div>
            <div class="col-12">
              <div class="form-check form-switch mb-2">
                <label class="text-left mb-2">Xuất bản</label>
                <input
                    v-model="modelContent.isPublic"
                    class="form-check-input"
                    type="checkbox"
                    id="flexSwitchCheckChecked"
                    checked=""
                />
              </div>
            </div>
            <div class="row">
              <div class="col-md-8" v-if="isShowMoTa==true">
                <div class="mb-2 ">
                  <label class="text-left mb-0">Mô tả</label>
                  <span style="color: red" v-if="isMoTa==true">&nbsp;*</span>
                  <textarea
                      v-model="modelContent.moTa"
                      id="percent"
                      type="text"
                      class="form-control mota"
                      placeholder="Nhập mô tả"
                      :class="{
                      'is-invalid':
                        submitted && $v.modelContent.moTa != null && $v.modelContent.moTa.$error,
                    }"
                  >
                  </textarea>
                  <div
                      v-if="submitted && $v.modelContent.moTa != null   && !$v.modelContent.moTa.required"
                      class="invalid-feedback"
                  >
                    Mô tả không được trống.
                  </div>
                </div>
              </div>

              <div class="col-md-4">
                <div class="col-12" v-if="isShowNgayXuatBan==true">
                  <div class="mb-2 ">
                    <label class="text-left mb-0">Ngày xuất bản</label>
                    <span style="color: red" v-if="isNgayXuatBan==true">&nbsp;*</span>
                    <date-picker
                        v-model="modelContent.ngayXuatBan"
                        format="DD/MM/YYYY HH:mm"
                        id="percent"
                        placeholder="Nhập ngày xuất bản"
                        :class="{
                        'is-invalid':
                          submitted && $v.modelContent.ngayXuatBan != null && $v.modelContent.ngayXuatBan.$error,
                      }"
                    >
                    </date-picker>
                    <div
                        v-if="submitted && $v.modelContent.ngayXuatBan != null   && !$v.modelContent.ngayXuatBan.required"
                        class="invalid-feedback"
                    >
                      Ngày xuất bản không được trống.
                    </div>
                  </div>
                </div>

                <div class="col-12" v-if="isShowAnhDaiDien==true">
                  <div class="mb-2 ">
                    <label for="formFileSm" class="text-left mb-0">Ảnh đại diện</label>
                    <span style="color: red" v-if="isAnhDaiDien">&nbsp;*</span>
                    <input
                        id="formFileSm"
                        name="file-input"
                        ref="fileInput"
                        type="file"
                        class="form-control"
                        @change="upload($event)"
                        :class="{
                        'is-invalid':
                          submitted && $v.modelContent.fileImage != null && $v.modelContent.fileImage.$error,
                      }"
                    />
                    <div
                        v-if="submitted && $v.modelContent.fileImage != null   && !$v.modelContent.fileImage.required"
                        class="invalid-feedback"
                    >
                      Ảnh đại diện không được trống.
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-8" v-if="isShowTrichYeu==true">
                <div class="mb-2 ">
                  <label class="text-left mb-0">Trích yếu</label>
                  <span style="color: red" v-if="isTrichYeu">&nbsp;*</span>
                  <textarea
                      v-model="modelContent.trichYeu"
                      id="percent"
                      type="text"
                      class="form-control mota"
                      placeholder="Nhập trích yếu"
                      :class="{
                        'is-invalid':
                          submitted && $v.modelContent.trichYeu != null && $v.modelContent.trichYeu.$error,
                      }"
                  >
                  </textarea>
                  <div
                      v-if="submitted && $v.modelContent.trichYeu != null   && !$v.modelContent.trichYeu.required"
                      class="invalid-feedback"
                  >
                    Trích yếu không được trống.
                  </div>
                </div>
              </div>

              <div class="col-md-4">
                <div class="col-12" v-if="isShowKyHieu==true">
                  <div class="mb-2 ">
                    <label class="text-left mb-0">Số ký hiệu</label>
                    <span style="color: red" v-if="isKyHieu">&nbsp;*</span>
                    <input
                        v-model="modelContent.soKiHieu"
                        id="percent"
                        type="text"
                        class="form-control"
                        placeholder="Nhập số ký hiệu"
                        :class="{
                          'is-invalid':
                            submitted && $v.modelContent.soKiHieu != null && $v.modelContent.soKiHieu.$error,
                        }"
                    />
                    <div
                        v-if="submitted && $v.modelContent.soKiHieu != null   && !$v.modelContent.soKiHieu.required"
                        class="invalid-feedback"
                    >
                      Số kí hiệu không được trống.
                    </div>
                  </div>
                </div>

                <div class="col-12" v-if="isShowNgayKy==true">
                  <div class="mb-2 ">
                    <label class="text-left mb-0">Ngày ký</label>
                    <span style="color: red" v-if="isNgayKy==true">&nbsp;*</span>
                    <date-picker
                        v-model="modelContent.ngayKy"
                        format="DD/MM/YYYY"
                        id="percent"
                        placeholder="Nhập ngày ký"
                        :class="{
                          'is-invalid':
                            submitted && $v.modelContent.ngayKy != null && $v.modelContent.ngayKy.$error,
                        }"
                    >
                    </date-picker>
                    <div
                        v-if="submitted && $v.modelContent.ngayKy != null   && !$v.modelContent.ngayKy.required"
                        class="invalid-feedback"
                    >
                      Ngày ký không được trống.
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div class="content-lis col-10" v-if="isShowNoiDung==true">
              <div class="cs-title-box">
                <div class="cs-title py-2">
                  <span class="fw-normal font-size-13">NỘI DUNG BÀI VIẾT</span>
                </div>
              </div>
            </div>
            <div v-if="isShowNoiDung==true">
<!--              <ckeditor v-model="modelContent.noiDung"-->
<!--                             :config="editorConfig"/>-->
              <ckeditor-nuxt v-model="modelContent.noiDung"
                             :config="editorConfig"  :class="{
                      'is-invalid':
                        submitted && $v.modelContent.noiDung != null && $v.modelContent.noiDung.$error,
                    }">

              </ckeditor-nuxt>
              <div
                  v-if="submitted && $v.modelContent.noiDung != null &&   !$v.modelContent.noiDung.required"
                  class="invalid-feedback"
              >
                Nội dung bài viết không được trống.
              </div>
            </div>
            <template v-if="isShowTepTin==true">
              <div style="margin-top: 20px;">
                <vue-dropzone
                    id="dropzone"
                    ref="myVueDropzone"
                    :use-custom-slot="true"
                    :options="dropzoneOptions"
                    v-on:vdropzone-sending="sendingEvent"
                    v-on:vdropzone-removed-file="removeThisFile"
                    v-on:vdropzone-success="uploadFile"
                >
                  <div class="dropzone-custom-content">
                    <div class="mb-1">
                      <i class="display-4 text-muted bx bxs-cloud-upload"></i>
                    </div>
                    <h4>Kéo thả tệp hoặc click vào đây để tải tệp tin.</h4>
                  </div>
                </vue-dropzone>
              </div>
            </template>

          </div>
        </form>

          <!-- <div class="row">
            <div class="col-md-12 text-center" >
              <b-button
                  class="btn cs-btn-primary btn-rounded mb-2 me-2"
                  variant="success"
                  v-on:click="handleSubmit"
              >
                <i class="bx bx-save "></i>
                Lưu
              </b-button>
            </div>
          </div> -->

      </div>
    </div>

  </Layout>
</template>
<style>

.hidden-sortable:after, .hidden-sortable:before {
  display: none !important;
}
.vue-treeselect__menu{
  max-height: 165px !important;
}
.btn-luu{
  background-color: #05912a;
  border: none;
}
.btn-luu:hover{
  background-color: #1da240;
}
.mx-table .mx-table-date .table thead th, thead, th {
  background-color: rgb(255, 255, 255)!important;
  accent-color: #0e0e0e !important;
  border: none;
}
.mota{
  height: 100px;
}
.mx-calendar-content .cell.active {
  color: #fff;
  background-color: #1284e7 !important;
}
.page-content{
  min-height: 800px;
}
</style>
