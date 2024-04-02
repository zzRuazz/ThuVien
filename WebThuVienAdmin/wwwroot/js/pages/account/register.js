async function register() {
    const fullname = $("#name").val();
    const address = $("#address").val();
    const email = $("#email").val();
    const password = $("#password").val();
    const repassword = $("#re-password").val();

    if (!fullname || !address || !email || !password || !repassword) {
        Swal.fire({
            title: "Error",
            text: "Data is required!",
            icon: "error",
        });
        return;
    };

    if (password !== repassword) {
        Swal.fire({
            title: "Error",
            text: "Password isn't matching!",
            icon: "error",
        });
        return;
    }

    const res = await AjaxProvider.Post({
        url: "/do-register",
        data: { email, password, address, fullname }
    });

    if (res.code < 0) {
        Swal.fire({
            title: "Error",
            text: res.message,
            icon: "error",
        });
        return;
    } else {
        Swal.fire({
            title: "Success",
            html: res.message,
            icon: "success"
        }).then(() => {
            window.location.href = '/';
        });
    }
}