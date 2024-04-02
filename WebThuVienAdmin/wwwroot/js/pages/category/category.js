let CategoryId = null;
const PageURL = "/danh-muc";

$(function () {
    $(".page-link").click(function (e) {
        e.preventDefault();
        let page = $(this).data("page");
        let limit = $("#perpage").val();
        console.log(page, limit);
        window.location.href = `/danh-muc/${page}/${limit}`
    })
})

async function Create() {
    let Name = $("#Name").val()
    let Slug = $("#Slug").val()
    let Image = $("#Image").val()
    let ParentId = $("#ParentId").val()
    let IsActived = $("#IsActived").val()
    let IsDeleted = !IsActived

    const category = { Name, Slug, Image, ParentId, IsActived, IsDeleted }

    const res = await AjaxProvider.Post({
        url: PageURL + "/tao-moi",
        data: category
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

function GenerateSlug() {
    let Name = $("#Name").val();
    $("#Slug").val(stringToSlug(Name));
}

async function Update(Id) {
    let Name = $("#Name").val();
    let Slug = $("#Slug").val();
    let Image = $("#Image").val();
    let ParentId = $("#ParentId").val();
    let IsActived = $("#IsActived").val();
    let IsDeleted = !IsActived;

    const category = { Id, Name, Slug, Image, ParentId, IsActived, IsDeleted }

    const res = await AjaxProvider.Post({
        url: PageURL + "/cap-nhat",
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
        }).then(() => {
            window.location.href = PageURL;
        });
    }
}

async function Edit(Id) {
    ClearModal();
    CategoryId = Id;

    const res = await AjaxProvider.Get({
        url: PageURL + "/chi-tiet/" + Id,
    });

    if (res.code == 0) {
        $("#Name").val(res.data.name)
        $("#Slug").val(res.data.slug)
        $("#Image").val(res.data.image)
        $("#ImageShow").attr("src", res.data.image)
        $("#ParentId").val(res.data.parentId)
        $("#IsActived").val(res.data.isActived + "")
    }
}

function ClearModal() {
    CategoryId = null;
    $("#Name").val("")
    $("#Slug").val("")
    $("#Image").val("")
    $("#ImageShow").attr("src", "")
    $("#ParentId").val("")
    $("#IsActived").val("")
}

function CreateOrUpdate() {
    if (CategoryId != null) {
        Update(CategoryId)
    } else {
        Create()
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

function ShowImage() {
    let Image = $("#Image").val();
    if (Image != "") {
        $("#ImageShow").attr("src", Image)
    }
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