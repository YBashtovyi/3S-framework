#! /usr/bin/python3
# -*- coding: utf-8 -*-

import os                       # used for path calls
import sys                      # used for sys.stdout and similar
import time                     # used in cases where need time (filename)
import subprocess               # used to run dotnet process
import telebot                  # used for telegram

def main():

    try:
        start = subprocess.Popen('dotnet test',
                        shell=True,
                        stdout=subprocess.PIPE, 
                        stderr=subprocess.STDOUT)

        # collect output information from subprocess amd attach it into log file
        output_info, _ = start.communicate()
        output_info = output_info.decode()

        #get exitcode of the command. If 0 = good. Else = bad.
        exitcode = start.wait()

        name = 'UnitTest-{}-{}.log'.format(os.environ.get("ENVIRONMENT"),time.strftime('%Y-%m-%d_%H-%M'))
        message = output_info[output_info.find('Total tests:'):]
        message = 'Unit tests [{}]\n{}'.format(os.environ.get("ENVIRONMENT"),message)

        print("Exitcode : {}".format(exitcode))
        print(name)
        print(message)

        bot = telebot.TeleBot(os.environ.get("TELEGRAM_BOT_TOKEN"))

        result = open(name,'w')

        # write into new temp file from memory 
        result.write(output_info)
        result.close()
        result = open(name,'rb')

        @bot.message_handler()

        def telegram(result,message):
            bot.send_message(os.environ.get("TELEGRAM_CHAT_ID"), message)
            bot.send_document(os.environ.get("TELEGRAM_CHAT_ID"), result)

        telegram(result,message)
        exit(0)

    except Exception as exception:
        print(exception)

if __name__ == "__main__":
    main()
