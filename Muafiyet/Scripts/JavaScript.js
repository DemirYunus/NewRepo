//$("#sifreDegiss").click(function () {
//    alert("1");
//    var email = document.getElementById("emailSifir").value;
//    $.ajax({
//        url: "/Uye/SifreSifirla",
//        type: "POST",
//        dataType: "json",
//        data: { email: email },
//        success: function (durum) {
//            if (durum == "başarılı") {
//                window.location.replace("/Uye/GirisYap");
//                $(".modal-body>p.hata").html("Yeni şifreniz belittiğiniz email adresine gönderildi.");
//            }
//            else if (durum == "hata") {
//                $(".modal-body>p.hata").html("Email hatalı veya internet bağlantısı koptu.");
//            }
//            else if (durum == "kayitliDegil") {
//                $(".modal-body>p.hata").html("Böyle bir email adresi kayıtlı değil");
//            }
//        }
//    });
//})


function sifirla(email) {

    $.ajax({
        type: "POST",
        url: "/Uye/SifreSifirla",
        data: { "email": email },
        success: function (durum) {
            if (durum == "başarılı") {
                window.location.replace("/Uye/GirisYap");
                $(".modal-body>p.hata").html("Yeni şifreniz belittiğiniz email adresine gönderildi.");
            }
            else if (durum == "hata") {
                $(".modal-body>p.hata").html("Email hatalı veya internet bağlantısı koptu.");
            }
            else if (durum == "kayitliDegil") {
                $(".modal-body>p.hata").html("Böyle bir email adresi kayıtlı değil");
            }
        },
        error: function () {
            alert("Bir Hata Oluştu !");
        }
    });
}


