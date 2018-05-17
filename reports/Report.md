# CHƯƠNG 1. HIỆN TRẠNG VÀ YÊU CẦU TỪ THỰC TẾ 

## Hiện trạng vấn đề 

### Vấn đề 
* Ngày nay, cùng với sự phát triển của đất nước, các ngành khoa học, ngành công nghệ thông tin,…. Đều sử dụng rất nhiều tài liệu tiếng anh. Trong hệ thống giáo dục của Việt Nam hầu hết các trường từ tiểu học, trung học, phổ thông đến các trường cao đẳng-đại học đều đào tạo bộ môn: Tiếng Anh.
* Do vậy, với mục tiêu nhằm hỗ trợ việc học cho học sinh, sinh viên, việc giảng dạy cho giáo viên ở các trường, hỗ trợ việc biên dịch tài liệu cho các ngành khoa học, công nghệ thông tin, các cơ quan chức năng…. Để đỡ tốn thời gian và công sức ngồi tra từng từ trong một cuốn từ điển dày mấy trăm trang, với sự phát triển không ngừng của ngành công nghệ thông tin các phần mềm từ điển Anh –Anh tiện dụng, hiệu quả cao ra đời ngày càng nhiều. 
* Ngôn ngữ lập trình là một phần không thể thiếu trong việc xây dựng nên một thế giới công nghệ linh hoạt và mạnh mẽ. Không gian làm việc Microsoft .Net tổng hợp bởi bốn bộ ngôn ngữ lập trình: C#, VB.NET, Managed C++, and J# .NET. ở đó có sự chồng gối lên nhau của các ngôn ngữ, và được định nghĩa trong FCL (framework class library). Hỗ trợ cho lập trình viên phát triển các ứng dụng mạng với kích thước nhẹ và mạnh mẽ trong xử lý.
* Dựa trên kiến thức lập trình mạng với C#, sự đa dạng của các dịch vụ mạng với nhiều tính năng và đòi hỏi ngày càng cao. Từ ý tưởng áp dụng công nghệ thông tin vào học tập, giúp cho việc học tập tiếng anh trở nên dễ dàng hơn, đồ án này hướng đến xây dựng một ứng dụng từ điển, áp dụng cho các nhu cầu học tập tiếng anh.
### Phương hướng giải quyết 
## Hiện trạng cơ sở vật chất và con người 
* Tin học 
* Con người         
## Yêu cầu sơ bộ phần mềm 
* Tin học 
* Con người 
# Chương 2. Phân tích yêu cầu phần mềm và mô hình hoá    
## Yêu cầu phần mềm 
### Yêu cầu chức năng 
* Bảng tổng hợp và định danh các yêu cầu:   

| Định danh  | Độ ưu tiên  |Mô tả yêu cầu                                                                         |
|------------|-------------|--------------------------------------------------------------------------------------|
| Yêu cầu 1  |             |Đọc dữ liệu từ database về thông tin các từ                                           |
| Yêu cầu 2  |             |Tìm kiếm từ vựng trong database của phần mềm                                          |
| Yêu cầu 3  |             |Hiển thị định nghĩa của từ khi người dùng tra từ                                      |
| Yêu cầu 4  |             |Phát âm từ vựng đã tra theo Anh–Anh và Anh-Mỹ                                         |
| Yêu cầu 5  |             |Khi tra một từ, gợi ý các từ loại khác liên quan (động từ, danh từ, tính từ...)       |
| Yêu cầu 6  |             |Autocomplete phần đuôi khi gõ phần đầu của từ cần tra                                 |
| Yêu cầu 7  |             |Khi nhập một từ tiếng anh không có trong từ điển, gợi ý các từ gần giống với từ đã tra|
| Yêu cầu 8  |             |Các từ đã tra được lưu vào một tab lịch sử                                            |
| Yêu cầu 9  |             |Chọn từ trong danh sách từ hiển thị ngay định nghĩa của từ                            |
| Yêu cầu 10 |             |Click vào một từ trong phần định nghĩa để dẫn đến định nghĩa của từ đó                |
### Yêu cầu phi chức năng 

|   Định danh	|   Độ ưu tiên	|   Mô tả yêu cầu	|
|---	        |---	        |---	            |
|   	        |   	        |   	            |
|   	        |   	        |   	            |
|   	        |   	        |   	            |

### Bảng FURPS

| Tiêu chí chất lượng  |  Mô tả                                                                                                 |
|---                   |---                                                                                                     |
| Functionality        | •	Chương trình hướng tới phục vụ người dùng đơn lẻ và hoàn toàn miễn phí                              |
| Usability            | •	Giao diện được thiết kể phẳng, hiện đại, đơn giản và dễ sử dụng, Chức năng tra cứu từ điển thông minh, tiện dụng, Cung cấp kèm theo tài liệu hướng dẫn và chức năng của chương trình                                                              |
| Reliability          | •	Chương trình lấy dữ liệu từ nguồn tin cậy (Oxford Learners Dictionaries) và được cập nhật liên tục  |
| Performance          | •	Việc tra cứu từ nhanh và chính xác, cách sắp xếp định nghĩa  dễ hiểu và khoa học                    |
| Supportability       | •	Database được tổ chức ở dạng chuẩn, sắp xếp khoa học                                                |

## Mô Hình hoá 
### Các trường hợp sử dụng thông thường 

|  Use Case | Tên         | Mô tả                           | Yêu cầu liên quan         |
|---        |---          |---                              |---                        |
| UC 1      | Tra cứu từ  | Tra cứu định nghĩa từ, phát âm  | Search, SpellCheck, Audio  |
|           |             |                                 |                           |
|           |             |                                 |                           |

#### Use case 1 (Tra cứu từ) 
![alt text](https://scontent.fsgn5-4.fna.fbcdn.net/v/t1.15752-9/32550241_656380638037814_973409897611788288_n.png?_nc_cat=0&_nc_eui2=AeEnVgEJX5A1aPRIa7Dk4ZQ7Ckxw8EgF9CSdSKyKqfDaFpyVkq6cwX0YR6fd_q-tt3sdfBzp5gSIpmuXFxT8yUm4jg8qU1wIbgyfP6g6l8iymQ&oh=6409cc9389ba2fffc4702d90e68c4192&oe=5B780998)