
const toJson = (item , listMenu) => {
    return {
        id: item.id,
        name: item.name,
        content: item.content,
        menu: item.menu,
        describe:item.describe,
        files: item.files,
        menuMobi : item.menuMobi,
        service : item.service,
        fileImage:item.fileImage,
        publicationDate  : item.publicationDate ,
        isPublish: item.isPublish,
    }
}
const fromJson = (item) => {
    return {
        id: item.id,
        name: item.name,
        content: item.content,
        menu: item.menu,
        describe:item.describe,
        files: item.files,
        menuMobi : item.menuMobi,
        service : item.service,
        fileImage:item.fileImage,
        publicationDate  : item.publicationDate ,
        isPublish: item.isPublish,
    }
}

const baseJson = () => {
    return {
        id: null,
        name: null,
        content: null,
        menu: null,
        describe:null,
        publicationDate : null,
        files:[],
        menuMobi : null,
        service : null,
        fileImage:null,
        isPublish: true,
    }
}


export const  newsModel = {
    toJson, fromJson, baseJson
}
