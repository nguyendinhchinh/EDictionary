# VE Dictionary

[![BSD 3-Clause License](https://img.shields.io/badge/License-BSD_3--Clauses-blue.svg?longCache=true)](https://github.com/NearHuscarl/E-Dictionary/blob/master/LICENSE.md)
[![Version](https://img.shields.io/badge/Version-1.6.10-green.svg?longCache=true)](https://github.com/NearHuscarl/E-Dictionary/releases)

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

### Data structures
#### Danh sách từ vựng
* Dictionary:
	* Đơn giản
	* Gần như không cần thiết lập vì đã có thư viện built-in sẵn
	* Tốc độ search rất nhanh, không phụ thuộc vào kích cỡ từ điển (O(1))
	* Không thể search prefix hiệu quả vì là unordered list
* Order List + Binary Search:
	* Đơn giản
	* Tốc độ search nhanh trong hầu hết trường hợp dù phụ thuộc vào kích cỡ từ điển (O(log n))
	* Có thể search prefix
* Prefix Tree:
	* Phức tạp hơn hai cách trên
	* Tốc độ search rât nhanh, vì chỉ phụ thuộc vào số ký tự trong 1 từ
	* Có thể search prefix
#### Định nghĩa
Có thể dùng dictionary với cặp (key, value) trong đó key là một thành phần
trong phần định nghĩa như nghĩa động từ, nghĩa danh từ, vd câu với động từ, vd
câu với danh từ... Và value là một mảng chứa giá trị tương ứng với mỗi thành
phần đó, nếu một từ có nhiều nghĩa động từ thì mảng sẽ chứa các từ tiếng việt
động từ tương ứng. VD:
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

### Dữ liệu
Không thể nhập bằng tay từ và định nghĩa của nó vì sẽ rất mất thời gian, với lại
đồ án này chỉ mang tính chất minh họa phục vụ cho môn học chứ không có sử dụng thực tế nên
các định nghĩa của mỗi từ sẽ được format đơn giản để có thể dùng 1 đoạn script dịch các từ từ danh sách [từ cơ bản này](http://www.greenteapress.com/thinkpython/code/words.txt), và tìm
các từ ví dụ tương ứng chứa từ đó

## Credit
Data structures:
* https://futurice.com/blog/data-structures-for-fast-autocomplete
* http://igoro.com/archive/efficient-auto-complete-with-a-ternary-search-tree/
* https://en.wikipedia.org/wiki/Trie
