async function login() {
    const email = $("#email").val();
    const password = $("#password").val();

    if (!email || !password) {
        Swal.fire({
            title: "Lỗi",
            text: "Data is required!",
            icon: "error",
        });
        return;
    };

    const res = await AjaxProvider.Post({
        url: "/do-login",
        data: { email, password }
    });

    if (res.code < 0) {
        Swal.fire({
            title: "Lỗi",
            text: res.message,
            icon: "error",
        });
        return;
    } else {
        Swal.fire({
            title: "Thông báo",
            html: res.message,
            icon: "success"
        }).then(() => {
            window.location.href = '/';
        });
    }
}