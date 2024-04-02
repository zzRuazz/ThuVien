let ProductPropertyId = null;
const PageURL = "/hang-san-xuat";

$(function () {
    $(".page-link").click(function (e) {
        e.preventDefault();
        let page = $(this).data("page");
        let limit = $("#perpage").val();
        console.log(page, limit);
        window.location.href = `${PageURL}/${page}/${limit}`
    })
})

async function Create() {
    let Name = $("#Name").val()
    let Slug = $("#Slug").val()
    let Website = $("#Website").val()
    let Image = $("#Image").val()
    let IsActived = $("#IsActived").val()
    let IsDeleted = !IsActived

    const property = { Name, Slug, Website, Image, IsActived, IsDeleted }

    const res = await AjaxProvider.Post({
        url: PageURL + "/tao-moi",
        data: property
    });

    if (res.code < 0) {
        Swal.fire({
            title: "Lỗi",
            text: "Thêm mới thất bại! Vui lòng kiểm tra lại!",
            icon: "error",
        });
        return;
    } else {
        Swal.fire({
            title: "Thông báo",
            html: "Thêm mới thành công!",
            icon: "success"
        }).then(() => {
            window.location.href = PageURL;
        });
    }
}

async function Edit(Id) {
    ClearModal();
    ProductPropertyId = Id;

    const res = await AjaxProvider.Get({
        url: PageURL + "/chi-tiet/" + Id,
    });

    if (res.code == 0) {
        $("#Name").val(res.data.name)
        $("#Slug").val(res.data.slug)
        $("#Website").val(res.data.website)
        $("#Image").val(res.data.image)
        $("#IsActived").val(res.data.isActived + "")
    }
}

async function Update(Id) {
    let Name = $("#Name").val();
    let Slug = $("#Slug").val();
    let Image = $("#Image").val();
    let Website = $("#Website").val();
    let IsActived = $("#IsActived").val();
    let IsDeleted = !IsActived;

    const property = { Id, Name, Slug, Image, Website, IsActived, IsDeleted }

    const res = await AjaxProvider.Post({
        url: PageURL + "/cap-nhat",
        data: property
    });

    if (res.code < 0) {
        Swal.fire({
            title: "Lỗi",
            text: "Cập nhật thất bại! Vui lòng kiểm tra lại!",
            icon: "error",
        });
        return;
    } else {
        Swal.fire({
            title: "Thông báo",
            html: "Cập nhật thành công!",
            icon: "success"
        }).then(() => {
            window.location.href = PageURL;
        });
    }
}

function CreateOrUpdate() {
    if (ProductPropertyId != null) {
        Update(ProductPropertyId)
    } else {
        Create()
    }
}

function ClearModal() {
    ProductPropertyId = null;
    $("#Name").val("")
    $("#Slug").val("")
    $("#Image").val("")
    $("#Website").val("")
    $("#IsActived").val("")
}

function GenerateSlug() {
    let Name = $("#Name").val();
    $("#Slug").val(stringToSlug(Name));
}

function stringToSlug(str) {
    // Chuyển hết sang chữ thường
    str = str.toLowerCase();

    // xóa dấu
    str = str
        .normalize('NFD') // chuyển chuỗi sang unicode tổ hợp
        .replace(/[\u0300-\u036f]/g, ''); // xóa các ký tự dấu sau khi tách tổ hợp

    // Thay ký tự đĐ
    str = str.replace(/[đĐ]/g, 'd');

    // Xóa ký tự đặc biệt
    str = str.replace(/([^0-9a-z-\s])/g, '');

    // Xóa khoảng trắng thay bằng ký tự -
    str = str.replace(/(\s+)/g, '-');

    // Xóa ký tự - liên tiếp
    str = str.replace(/-+/g, '-');

    // xóa phần dư - ở đầu & cuối
    str = str.replace(/^-+|-+$/g, '');

    // return
    return str;
}

function ShowImage() {
    let Image = $("#Image").val();
    if (Image != "") {
        $("#ImageShow").attr("src", Image)
    }
}

async function Active(Id, status) {
    const category = { Id, IsActived: status }

    const res = await AjaxProvider.Post({
        url: PageURL + "/cap-nhat-trang-thai",
        data: category
    });

    if (res.code < 0) {
        Swal.fire({
            title: "Lỗi",
            text: "Cập nhật thất bại! Vui lòng kiểm tra lại!",
            icon: "error",
        });
        return;
    } else {
        Swal.fire({
            title: "Thông báo",
            html: "Cập nhật thành công!",
            icon: "success"
        }).then(function () {
            location.reload()
        });
    }
}

async function Delete(Id) {
    const res = await AjaxProvider.Get({
        url: PageURL + "/xoa/" + Id
    });

    if (res.code < 0) {
        Swal.fire({
            title: "Lỗi",
            text: "Xoá thất bại! Vui lòng kiểm tra lại!",
            icon: "error",
        });
        return;
    } else {
        Swal.fire({
            title: "Thông báo",
            html: "Xoá thành công!",
            icon: "success"
        }).then(function () {
            location.reload()
        });
    }
}