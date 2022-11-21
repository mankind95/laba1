from multiprocessing import Process, cpu_count
from hashlib import sha256

hackmd_hashes = ['1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad',
                 '3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b',
                 '74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f']

alphabet = 'abcdefghijklmnopqrstuvwxyz'


def bruter(_hash):
    _pass = ""
    for a in alphabet:
        for b in alphabet:
            for c in alphabet:
                for d in alphabet:
                    for e in alphabet:
                        _pass = a + b + c + d + e
                        encoded_hash = sha256(_pass.encode()).hexdigest()

                        if encoded_hash == _hash:
                            print(_pass)
                            print(_hash)
                            break

class Menu():
    def __init__(self):
        self.hashik = []
        self.setMode(0)

    def setMode(self, hashik):
        print("1 - ручной ввод")
        print("2 - чтение из файла")
        print("3 - задание с hackmd")

        _mode = int(input())

        if _mode == 1:
            self.hashik.append(input())
            self.userThreadMode()

        elif _mode == 2:
            self.fileReda()
            self.userThreadMode()

        elif _mode == 3:
            self.hashik = hackmd_hashes
            self.userThreadMode()

    def userThreadMode(self):
        print("\n1 - однопоточныйы\n")
        print("\n2 - многопоточный\n")

        _choice = int(input())
        if _choice == 1:
            OneThread(self.hashik)

        if _choice == 2:
            MultiThread(self.hashik)

    def fileReda(self):
        path = input("\nВведите путь до файла"
                     "\n>>> ")

        with open(path, 'r', encoding='latin-1') as hashes:

            for _hash in hashes:
                _hash = _hash.replace('\n', '')

                if _hash != '' and len(_hash) == 64:
                    self.hashik.append(_hash)
            print(self.hashik)



class OneThread:
    def __init__(self, _hashes):
        self.hashes = _hashes

        for _hash in self.hashes:
            bruter(_hash)

class MultiThread:
    def __init__(self, _hashes):
        self.hashes = _hashes
        self.lenHesh = len(self.hashes)
        self.n_cpus = cpu_count()
        self.n_threads = None
        self.userMode()

    def setThreads(self):
        threads = []
        temp_n_threads = 0

        for i in range(self.lenHesh):
            temp_n_threads += 1
            thread = Process(target=bruter, args=(self.hashes[i],))
            threads.append(thread)
            thread.start()

            if temp_n_threads < self.n_threads:
                continue

            else:
                for thread in threads:
                    thread.join()
                temp_n_threads = 0

        for thread in threads:
            thread.join()

        return

    def userMode(self):
        print(f"\n Доступно {self.n_cpus} ядер для {self.lenHesh} хэшей")
        print("Введите кол-во потоков для работы программы")

        choice = int(input())
        if choice <= self.n_cpus and choice <= self.lenHesh:
            self.n_threads = choice
            self.setThreads()

        if self.n_cpus >= choice > self.lenHesh:
            self.n_threads = self.lenHesh
            self.setThreads()



if __name__ == "__main__":
    Menu()
