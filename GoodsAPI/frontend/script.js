const apiUrl = "http://localhost:5238/api/HangHoa";

// 🟢 Lấy danh sách hàng hóa từ API
async function fetchGoods() {
    try {
        const response = await fetch(apiUrl);
        if (!response.ok) throw new Error("Lỗi khi lấy dữ liệu");

        const goods = await response.json();
        const goodsList = document.getElementById("goodsList");
        goodsList.innerHTML = ""; 

        goods.forEach(hangHoa => {
            const row = `<tr>
                <td>${hangHoa.MaHangHoa}</td>
                <td>${hangHoa.TenHangHoa}</td>
                <td>${hangHoa.SoLuong}</td>
                <td>${hangHoa.GhiChu || ""}</td>
                <td>
                    <button onclick="updateGhiChu('${hangHoa.MaHangHoa}')">Cập nhật ghi chú</button>
                    <button onclick="deleteGoods('${hangHoa.MaHangHoa}')">Xóa</button>
                </td>
            </tr>`;
            goodsList.innerHTML += row;
        });
    } catch (error) {
        console.error("Lỗi:", error);
    }
}

// 🟢 Thêm hàng hóa mới
document.getElementById("addForm").addEventListener("submit", async function (event) {
    event.preventDefault();

    const hangHoa = {
        MaHangHoa: document.getElementById("maHangHoa").value,
        TenHangHoa: document.getElementById("tenHangHoa").value,
        SoLuong: parseInt(document.getElementById("soLuong").value),
        GhiChu: document.getElementById("ghiChu").value
    };

    try {
        const response = await fetch(apiUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(hangHoa)
        });

        if (!response.ok) throw new Error("Lỗi khi thêm hàng hóa");
        this.reset();
        fetchGoods();
    } catch (error) {
        console.error("Lỗi:", error);
    }
});

// 🟢 Cập nhật ghi chú
async function updateGhiChu(maHangHoa) {
    const ghiChu = prompt("Nhập ghi chú mới:");
    if (!ghiChu) return;

    try {
        const response = await fetch(`${apiUrl}/${maHangHoa}/ghichu`, {
            method: "PATCH",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(ghiChu)
        });

        if (!response.ok) throw new Error("Lỗi khi cập nhật ghi chú");
        fetchGoods();
    } catch (error) {
        console.error("Lỗi:", error);
    }
}

// 🔴 Xóa hàng hóa
async function deleteGoods(maHangHoa) {
    if (!confirm("Bạn có chắc chắn muốn xóa?")) return;

    try {
        const response = await fetch(`${apiUrl}/${maHangHoa}`, { method: "DELETE" });

        if (!response.ok) throw new Error("Lỗi khi xóa hàng hóa");
        fetchGoods();
    } catch (error) {
        console.error("Lỗi:", error);
    }
}

// 🚀 Tải danh sách hàng hóa khi trang được mở
fetchGoods();
