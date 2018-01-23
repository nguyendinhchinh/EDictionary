# VE Dictionary
Đồ án môn lập trình trực quan, trường đại học CNTT

# Roadmap

VE Dictionary là từ điển Anh Việt, dùng để tra các từ tiếng anh sang nghĩa tiếng việt

## Features
* Tra các từ tiếng Anh
* Phát âm các từ đã tra
* Mỗi từ đều có ví dụ cách sử dụng của từ đó bằng một câu tiếng anh mẫu
* Có thể click vào một từ trong phần định nghĩa để dẫn đến định nghĩa của từ đó
* Cập nhật các từ tiếng anh mới
* Sửa hoặc bổ sung các từ đã có trong từ điển
* Autocomplete phần đuôi khi gõ phần đầu của từ cần tra
* Khi tra một từ, gợi ý các từ loại khác liên quan (động từ, danh từ, tính từ...)
* Khi nhập một từ tiếng anh không có trong từ điển, gợi ý các từ gần giống với từ đã tra
* Sau mỗi một khoảng thời gian, pop up ở taskbar một từ trong danh sách để học t.a
* Các từ đã tra được lưu vào một tab lịch sử
## Design

* Cấu trúc dữ liệu: Sử dụng bảng hash (hay từ điển) để chứa các từ tiếng anh và định nghĩa với
key là từ tiếng anh và value là một kiểu dữ liệu (có thể là 1 bảng hash khác) dùng để chứa
phần nghĩa. VD:
```python
ve_dictionary_data = {
    'apple': {
        'noun': ['táo'],
        'noun_example': ['Peel and core the apple'],
        'verb': [],
        'verb_example': [],
        'abverb': [],
        'abverb_example': [],
        'adj': [],
        'adj_example': [],
        'audio': 'path/to/audio/file',
    },
    'bank': {
        'noun': ['ngân hàng', 'tiền cược'],
        'noun_example': [
            'My salary is paid directly into my bank',
            '',
        ],
        'verb': ['chứa'],
        'verb_example': [],
        ...
    },
    ...
}
```
* Dữ liệu: không thể nhập bằng tay từ và định nghĩa của nó vì sẽ rất mất thời gian, với lại
đồ án này chỉ mang tính chất minh họa phục vụ cho môn học chứ không có sử dụng thực tế nên
các định nghĩa của mỗi từ sẽ được format đơn giản để có thể dùng 1 đoạn script dịch các từ từ danh sách [từ cơ bản này](http://www.greenteapress.com/thinkpython/code/words.txt), và tìm
các từ ví dụ tương ứng chứa từ đó
