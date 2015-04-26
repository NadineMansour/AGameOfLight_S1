class Question < ActiveRecord::Base
  belongs_to :teacher
  belongs_to :subject
  has_many :student_answer_questions
end
