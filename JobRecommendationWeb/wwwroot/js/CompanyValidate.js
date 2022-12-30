$("#formCreate").validate({
    rules: {
        TenCongTy: {
            required: true,
        },
        DiaChi: {
            required: true
        },
        Website: {
            required: true,
            url: true,
        },
        country: {
            required: true,
        },
        AnhCongTy: {
            required: true,
        },
    },
    messages: {
        TenCongTy: {
            required: "Vui lòng nhập tên công ty"
        },
        DiaChi: {
            required: "Vui lòng nhập địa chỉ"
        },
        Website: {
            required: "Vui lòng nhập website công ty",
            url: "Vui lòng nhập đúng cú pháp",
        },
        country: {
            required: "Vui lòng chọn quốc gia"
        },
        AnhCongTy: {
            required: "Vui lòng chọn ảnh logo"
        },
    },
    submitHandler: function (form) {
        form.submit();
    }
});