$("#formValidation").validate({
    rules: {
        account: {
            minlength: 2
        },
        password: {
            minlength: 3
        },
        password_confirmation: {
            minlength: 3
        },
        name: {
            minlength: 2
        },
        phone: {
            number: true,
            minlength: 10,
            maxlength: 10
        },
        age: {
            number: true,
            minlength: 2,
            maxlength: 2
        },
        email: {
            email: true
        }
    },
    messages: {
        account: {
            required: "Hãy nhập tên đăng nhập",
            minlength: "Tên đăng nhập phải có tối thiểu 2 ký tự"
        },
        password: {
            required: "Hãy nhập mật khẩu",
            minlength: "Mật khẩu phải có tối thiểu 3 ký tự"
        },
        password_confirmation: {
            required: "Hãy nhập lại mật khẩu",
            minlength: "Mật khẩu phải có tối thiểu 3 ký tự"
        },
        name: {
            required: "Hãy nhập họ tên",
            minlength: "Tên phải có tối thiểu 2 ký tự"
        },
        email: "Hãy nhập email",
        phone: {
            required: "Hãy nhập số điện thoại",
            minlength: "Số điện thoại phải có 10 ký tự",
            maxlength: "Số điện thoại phải có 10 ký tự"
        },
        age: "Hãy nhập tuổi",
        position: "Hãy chọn chức vụ"
    },


    submitHandler: function (form) {
        form.submit();
    }
});

// reset form
function resetForm() {
    document.getElementsByTagName('form')[0].reset();
}
