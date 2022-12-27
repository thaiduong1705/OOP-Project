$("#formCreate").validate({
    rules: {
        MaCongTy: {
            required: true,
        },
        TenCongViec: {
            required: true,
        },
        LuongMin: {
            required: true,
            digits: true,
            min: 1000
        },

        LuongMax: {
            required: true,
            digits: true,
            min: 1000
        },

        ThamNien: {
            required: true,
            digits: true
        },
        CapBac: {
            required: true,
        },

        KiNang: {
            required: true,
        },

        WebsiteBaiGoc: {
            required: true,
            url: true
        }
    },
    messages: {
        MaCongTy: {
            required: "Vui lòng chọn tên công ty"
        },
        TenCongViec: {
            required: "Vui lòng nhập tên công việc"
        },
        LuongMin: {
            required: "Vui lòng nhập lương tối thiểu",
            digits: "Vui lòng nhập đúng kiểu số lớn hơn 0",
            min: "Nhập tối thiểu là 1000"
        },

        LuongMax: {
            required: "Vui lòng nhập lương tối đa",
            digits: "Vui lòng nhập đúng kiểu số lớn hơn 0",
            min: "Nhập tối thiểu là 1000"
        },

        ThamNien: {
            required: "Vui lòng nhập số năm kinh nghiệm",
            digits: "Vui lòng nhập đúng kiểu số lớn hơn 0"
        },

        CapBac: {
            required: "Vui lòng nhập cấp bậc",
        },

        WebsiteBaiGoc: {
            required: "Vui lòng nhập website bài tuyển dụng gốc",
            url: "Vui lòng nhập website bài tuyển dụng gốc",
        },

        KiNang: {
            required: "Vui lòng nhập ít nhất 1 kĩ năng",
        },
    },
    submitHandler: function (form) {
        form.submit();
    }
});