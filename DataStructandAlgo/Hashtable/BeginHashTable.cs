using System;
using System.Collections.Generic;

namespace HashTableTest
{
    class Book // 데이터 value
    {
        public string bookName;
        public int position;

        public Book(string name, int pos)
        {
            this.bookName = name;
            position = pos;
        }
    }

    class Hash // 해시값
    {
        public Book _data; // 데이터 value
        public bool isCreateList; // 리스트를 만들었나?
        private List<Book> indexList; // 데이터가 2개 이상일 시 생성

        private void InitList() // 리스트 생성
        {
            indexList = new List<Book>();
            isCreateList = true;

            indexList.Add(_data); // 기존 데이터를 넣기
        }

        public void AddData(Book data) // 해당 해시에 데이터 저장
        {
            if (_data == null) // 데이터가 없으면
            {
                _data = data;
                return;
            }
            else if(isCreateList == false)
            {
                InitList();
            }

            indexList.Add(data);
        }

        public Book FindData(string bookName) // 해당 해시에서 이름으로 데이터 탐색
        {
            if (isCreateList == false)
            {
                return _data;
            }

            for (int i = 0; i < indexList.Count; i++)
            {
                if (indexList[i].bookName == bookName)
                    return indexList[i];
            }

            Console.WriteLine("못찾음");
            return null;
        }
    }

    class HashTable // 해시테이블
    {
        Hash[] hashTable = new Hash[10]; // 해시 테이블 크기 : 10

        public HashTable()
        {
            for (int i = 0; i < hashTable.Length; i++)
            {
                hashTable[i] = new Hash();
            }
        }

        public int HashFunction(string value) // 임의의 해시 함수
        {
            return (value.Length * 13 + 43) % hashTable.Length; // 원래 이것보다는 좋은 함수가 많다
        }

        public void AddHash(Book data) // 해시테이블에 추가
        {
            int hash = HashFunction(data.bookName);

            hashTable[hash].AddData(data);
        }

        public Book FindHash(string bookName) // 해시테이블에서 탐색
        {
            int index = HashFunction(bookName);

            return hashTable[index].FindData(bookName);
        }
    }

    class MainStart
    {
        static void Main(string[] args)
        {
            HashTable hashT = new HashTable();

            // 해시테이블에 자료 저장
            Book[] books = { new Book("책1", 2495), new Book("책2", 2912), new Book("책3", 1983), new Book("책4", 7215), new Book("책5",  6823),
                             new Book("책6", 7493), new Book("책7", 3484), new Book("책8", 2734), new Book("책9", 1243), new Book("책10", 4928)};

            for (int i = 0; i < books.Length; i++)
            {
                hashT.AddHash(books[i]);
            }

            // 해시테이블에서 자료 탐색
            Book book = hashT.FindHash("책1");
            if (book != null)
            {
                Console.WriteLine(book.position);
            }
            else
            {
                Console.WriteLine("자료가 해시테이블에 없음");
            }
        }
    }
}
