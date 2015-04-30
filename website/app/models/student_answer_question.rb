class StudentAnswerQuestion < ActiveRecord::Base
	belongs_to :student
	belongs_to :question
  before_create :set_correctness


  private


  def set_correctness
    self.correct = self.question.correct_answer == self.answer
    #return true to avoid rollback if correct = false
    true
  end
end
