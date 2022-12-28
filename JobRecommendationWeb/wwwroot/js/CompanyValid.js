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
        logo: {
            required: true,
        },
        CheDoDaiNgo: {
            required: true
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
        logo: {
            required: "Vui lòng chọn ảnh logo"
        },
        CheDoDaiNgo: {
            required: "Vui lòng nhập"
        },
    },
    submitHandler: function (form) {
        form.submit();
    }
});