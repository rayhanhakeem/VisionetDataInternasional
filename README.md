1. Statement Test — Code Analysis & Optimization
   Answer 1 :
   - Nested Loop membuat kode lambat
   - Menghasilkan duplikat
     
   Answer 2 :
   public List<(int, int)> FindPairsOptimized(List<int> numbers, int target)
   {
       var result = new List<(int, int)>();
       var seen = new HashSet<int>();
       var unique = new HashSet<(int, int)>();

       foreach (var num in numbers)
       {
           int complement = target - num;

           if (seen.Contains(complement))
           {
               var pair = (Math.Min(num, complement), Math.Max(num, complement));
               if (!unique.Contains(pair))
               {
                   result.Add(pair);
                   unique.Add(pair);
               }
           }

           seen.Add(num);
       }

       return result;
   }

   Answer 3 :
   Saya memastikan urutan selalu disusun min max cth: (3,2) = (2,3) dan HashSet unique pairs menyimpan pasangan yang sudah pernah ditemukan.
   Kombinasi ini menghilangkan duplikat sepenuhnya.
   
2. Logical Test — Concurrency & Thread Safety
   Answer 1 : Race Condition

   Answer 2 : Menggunakan Interlocked

   Answer 3 :
   Di distributed environment, kita tidak bisa mengandalkan variabel in-memory karena setiap node memiliki memorinya sendiri.
   Untuk menjaga konsistensi, kita perlu menggunakan data store yang mendukung atomic operations seperti Redis (INCR), SQL (UPDATE dengan set value=value+1), atau menggunakan distributed lock.
   Bisa juga dengan message queue agar semua update dilakukan oleh satu consumer yang konsisten.
   
3. Data Integrity Test — Transactional Consistency & UI

