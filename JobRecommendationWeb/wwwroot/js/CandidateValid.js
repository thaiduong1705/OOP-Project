$.validator.addMethod("needsSelection", function (value, element) {
    var count = $(element).find('option:selected').length;
    return count > 0;
}, "Vui lòng chọn ít nhất 1 kĩ năng");
$("#formCreate").validate({
    rules: {
        Ten: {
            required: true,

        },
        Tuoi: {
            required: true,
            range: [15, 65],
            number: true
        },
        ThamNien: {
            required: true,
            number: true
        },
        Sdt: {
            required: true
        },
        Email: {
            email: true,
            required: true,
        },
        CV: {
            required: true,
        },
        test: {
            required: true,
            needsSelection: true
        },
        DiaChi: {
            required: true
        },
        country: {
            required: true,
        },


        TenCongTy: {
            required: true

        },
    },
    messages: {
        Ten: {
            required: "Nhập tên",

        },
        Tuoi: {
            required: "Nhập tuổi",
            number: "Chỉ nhập số",
            range: "Tuổi từ 18 tới 65",
        },
        ThamNien: {
            required: "Nhập thâm niên",
            number: "Chỉ nhập số"
        },
        Sdt: {
            required: "Nhập số điện thoại"
        },
        Email: {
            email: "Nhập đúng cú pháp email",
            required: "Nhập email",
        },
        CV: {
            required: "Vui lòng chọn CV"
        },
        Kinang: {
            required: "Vui lòng chọn kĩ năng",
           
        },
        DiaChi: {
            required: "Vui lòng nhập địa chỉ"
        },
        country: {
            required: "Vui lòng chọn quốc tịch"
        }
    },
    submitHandler: function (form) {
        form.submit();
    }
});

