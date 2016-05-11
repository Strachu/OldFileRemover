# OldFileRemover
**OldFileRemover** is an utility for removing old files from a directory. When executed the application will compare the size of directory to a specified quota. When the directory exceeds the quota, the application will start to remove files starting from the oldest till the quota is meet.

# Usage example
To automatically run the application every hour on Linux use cron.

Firstly start `crontab -e` command, the add the following line at the end of file:

0 * * * * `/usr/bin/mono /path/OldFileRemover.exe --exclude "^\." --max-size 1073741824 /path/to/directory`

This will make the application run every hour starting from 1:00, removing files from directory on path /path/to/directory when its size exceeds 1GiB. The files starting with a dot will be ignored.
